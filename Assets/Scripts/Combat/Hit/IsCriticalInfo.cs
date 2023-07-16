namespace Combat.Hit
{
    public struct IsCriticalInfo
    {
        public bool Value { get; private set; }
        public static IsCriticalInfo Create(bool value) => new IsCriticalInfo {Value = value};
    }
}