using Amazon.CDK;

namespace InfrastructureAsCode;

class Program
{
    static void Main(string[] args)
    {
        App app = new();
        app.Synth();
    }
}