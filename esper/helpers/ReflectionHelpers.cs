using System.Reflection;

namespace esper.helpers {
    public static class ReflectionHelpers {
        public static IEnumerable<Type> GetClasses(Func<Type, bool> typeFilter) {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(typeFilter);
        }
    }
}
