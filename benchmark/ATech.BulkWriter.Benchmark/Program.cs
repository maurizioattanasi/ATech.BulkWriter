using ATech.BulkWriter.Benchmark;

using BenchmarkDotNet.Running;

DbHelpers.SetupDb();
BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

DbHelpers.DisposeDatabase();

Console.WriteLine("Ended benchmark");