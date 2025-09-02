﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using esper;
using esper.plugins;
using esper.setup;
using esper.resolution;
using esper.elements;
using esper.data;
using System.Diagnostics;

namespace Benchmarks {
    class Program {
        public Session session;
        public PluginManager pluginManager => session.pluginManager;
        public ResourceManager resourceManager => session.resourceManager;
        public PluginFile plugin;

        public static string fixturesPath = Path.Combine(
            Environment.CurrentDirectory,
            "fixtures"
        );

        public static string FixturePath(string filename) {
            return Path.Combine(fixturesPath, filename);
        }

        private List<string> GetStringFiles() {
            var skyrimStringsPath = FixturePath("skyrim_strings");
            var stringFiles = Directory.GetFiles(
                skyrimStringsPath, "skyrim_english.*"
            ).ToList();
            return stringFiles;
        }

        private void LoadSkyrimEsm() {
            var pluginPath = FixturePath("Skyrim.esm");
            plugin = pluginManager.LoadPlugin(pluginPath);
        }

        public void SetUp() {
            session = new Session(Games.TES5, new SessionOptions());
            LoadSkyrimEsm();
            resourceManager.LoadPluginStrings(plugin, GetStringFiles());
        }

        public void TouchSubrecords(string signature, string path) {
            var records = plugin.GetElements(signature);
            foreach (var record in records)
                record.GetValue(path);
        }

        public void BuildReferencedBy() {
            var groupedRecords = new Dictionary<Signature, List<MainRecord>>();
            foreach (var record in plugin.records) {
                var sig = record.signature;
                if (sig.ToString() == "NAVM") continue;
                if (!groupedRecords.ContainsKey(sig))
                    groupedRecords.Add(sig, new List<MainRecord>());
                groupedRecords[sig].Add(record);
            }
            var watch = new Stopwatch();
            foreach (var sig in groupedRecords.Keys) {
                Console.WriteLine($"Building references for {sig} records.");
                watch.Reset();
                watch.Start();
                foreach (var rec in groupedRecords[sig]) {
                    try {
                        rec.BuildRefBy();
                    } catch (Exception x) {
                        Console.WriteLine($"Error occurred when building references for {rec.path}");
                        Console.WriteLine(x.Message);
                    }
                }
                watch.Stop();
                Console.WriteLine($"{watch.ElapsedMilliseconds}ms spent building references for {sig} records.");
            }
        }

        static void Main(string[] args) {
            var watch = new Stopwatch();
            var p = new Program();
            watch.Start();
            p.SetUp();
            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds}ms spent setting up.");
            watch.Reset();
            Console.WriteLine("Building referenced by... (may take up to 30 seconds)");
            watch.Start();
            p.BuildReferencedBy();
            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds}ms spend building referenced by for Skyrim.esm");
            watch.Reset();
            watch.Start();
            p.TouchSubrecords("WEAP", "FULL");
            p.TouchSubrecords("NPC_", "FULL");
            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds}ms spend getting FULL values from records in Skyrim.esm");
        }
    }
}
