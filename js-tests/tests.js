const dotnet = require('node-api-dotnet');
const esper = dotnet.require('./esper/esper');
const path = require('path');

const { Games, Session, SessionOptions } = esper;

let session = new Session(Games.TES5, new SessionOptions());
let pluginPath = path.resolve('../Fixtures/AllRecords.esp');
let plugin = session.pluginManager.loadPlugin(pluginPath);
console.log(plugin.name);
console.log(plugin.records.length);