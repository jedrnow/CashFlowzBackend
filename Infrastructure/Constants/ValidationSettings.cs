namespace CashFlowzBackend.Infrastructure.Constants
{
    public static class ValidationSettings
    {
        public static readonly int MinLoginLength = 6;
        public static readonly int MaxLoginLength = 18;
        public static readonly int MinPasswordLength = 8;
        public static readonly int MaxPasswordLength = 20;
        public static readonly int MinEmailLength = 3;
        public static readonly int MaxEmailLength = 128;
        public static readonly int MinNameLength = 3;
        public static readonly int MaxNameLength = 128;
    }
}
