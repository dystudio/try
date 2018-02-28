using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Recipes;
using WorkspaceServer.Models.Execution;
using Workspace = MLS.Agent.Tools.Workspace;

namespace WorkspaceServer.Tests
{
    public static class Create
    {
        public static async Task<Workspace> TestWorkspace([CallerMemberName] string testName = null)
        {
            var workspace = Workspace.Copy(
                await Default.TemplateWorkspace,
                testName);

            await workspace.EnsureCreated();
            await workspace.EnsureBuilt();

            return workspace;
        }

        public static WorkspaceRequest SimpleRunRequest(
            string consoleOutput = "Hello!",
            string workspaceType = null) => new WorkspaceRequest(SimpleWorkspace(consoleOutput, workspaceType));

        public static Models.Execution.Workspace SimpleWorkspace(
            string consoleOutput = "Hello!",
            string workspaceType = null) => new Models.Execution.Workspace(SimpleConsoleAppCodeWithoutNamespaces(consoleOutput), workspaceType: workspaceType);

        public static string SimpleWorkspaceAsJson(
            string consoleOutput = "Hello!",
            string workspaceType = null)
        {
            return new
            {
                buffer = SimpleConsoleAppCodeWithoutNamespaces(consoleOutput),
                workspaceType
            }.ToJson();
        }

        public static string SimpleConsoleAppCodeWithoutNamespaces(string consoleOutput)
        {
            return $@"
using System;

public static class Hello
{{
    public static void Main()
    {{
        Console.WriteLine(""{consoleOutput}"");
    }}
}}";
        }
    }
}
