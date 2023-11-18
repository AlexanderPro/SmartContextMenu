namespace SmartContextMenu
{
    class ProcessInfo
    {
        public string CommandLine { get; set; }

        public uint HandleCount { get; set; }

        public uint ThreadCount { get; set; }

        public ulong VirtualSize { get; set; }

        public ulong WorkingSetSize { get; set; }

        public string Owner { get; set; }
    }
}
