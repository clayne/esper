﻿using esper.parsing;
using esper.plugins;
using System;
using System.Collections.Generic;
using System.IO;

namespace esper.setup {
    public class PluginManager {
        public Game game;
        public Session session;
        public bool usingLightPlugins;
        public int maxLightPluginIndex;
        public int maxFullPluginIndex;
        public List<PluginFile> plugins;
        public List<FullPluginSlot> fullPluginSlots;
        public List<LightPluginSlot> lightPluginSlots;

        public int nextLightPluginIndex {
            get {
                int index = lightPluginSlots.Count;
                if (index > maxLightPluginIndex)
                    throw new Exception("Maximum light plugins exceeded.");
                return index;
            }
        }

        public int nextFullPluginIndex {
            get {
                int index = fullPluginSlots.Count;
                if (index > maxFullPluginIndex)
                    throw new Exception("Maximum full plugins exceeded.");
                return index;
            }
        }

        public PluginManager(Game game, Session session) {
            this.game = game;
            this.session = session;
            usingLightPlugins = session.options.allowLightPlugins &&
                game.SupportsLightPlugins();
            maxLightPluginIndex = usingLightPlugins ? 4095 : 0;
            maxFullPluginIndex = usingLightPlugins ? 253 : 254;
        }

        public bool ShouldUseLightPluginSlot(PluginFile plugin) {
            return usingLightPlugins && plugin.IsEsl();
        }

        public void AssignLoadOrder(PluginFile plugin) {
            if (ShouldUseLightPluginSlot(plugin)) {
                lightPluginSlots.Add(
                    new LightPluginSlot(plugin, nextLightPluginIndex)
                );
            } else {
                fullPluginSlots.Add(
                    new FullPluginSlot(plugin, nextFullPluginIndex)
                );
            }
        }

        public void AddFile(PluginFile plugin) {
            if (plugin.options.temporary) return;
            plugins.Add(plugin);
            if (!session.options.emulateGlobalLoadOrder) return;
            AssignLoadOrder(plugin);
        }

        public PluginFile CreateDummyPlugin(string filename) {
            return new PluginFile(session, filename, new PluginFileOptions{});
        }

        public PluginFile GetFileByName(
            string filename, 
            bool createDummyIfMissing = false
        ) {
            foreach (var plugin in plugins)
                if (plugin.filename == filename) return plugin;
            if (!createDummyIfMissing) return null;
            return CreateDummyPlugin(filename);
        }

        public List<string> GetMasterFileNames(string filePath) {
            PluginFileOptions options = new PluginFileOptions {
                temporary = true
            };
            var filename = Path.GetFileName(filePath);
            PluginFile plugin = new PluginFile(session, filename, options);
            new PluginFileSource(filePath, plugin);
            plugin.ReadFileHeader();
            return plugin.GetMasterFileNames();
        }
    }
}