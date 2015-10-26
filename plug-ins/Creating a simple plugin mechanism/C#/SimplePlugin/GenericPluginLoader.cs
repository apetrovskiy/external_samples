/*
   Copyright 2013 Christoph Gattnar

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

	   http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

namespace SimplePlugin
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Linq;

    public static class GenericPluginLoader<T>
	{
		public static ICollection<T> LoadPlugins(string path)
		{
		    if (!Directory.Exists(path)) return null;
		    var dllFileNames = Directory.GetFiles(path, "*.dll");

		    var assemblies = new List<Assembly>(dllFileNames.Length);
		    assemblies.AddRange(dllFileNames.Select(AssemblyName.GetAssemblyName).Select(Assembly.Load));

		    var pluginType = typeof(T);
		    var pluginTypes = (from assembly in assemblies where assembly != null select assembly.GetTypes() into types from type in types.Where(type => !type.IsInterface && !type.IsAbstract).Where(type => type.GetInterface(pluginType.FullName) != null) select type).ToList();

		    var plugins = new List<T>(pluginTypes.Count);
		    plugins.AddRange(pluginTypes.Select(type => (T) Activator.CreateInstance(type)));

		    return plugins;
		}
	}
}
