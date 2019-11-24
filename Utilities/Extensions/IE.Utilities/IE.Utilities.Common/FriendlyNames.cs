namespace IE.Utilities.Common
{
    public static class FriendlyNames
    {
        public static string SoundEffectName(this SoundEffectType effectType)
        {
            switch (effectType)
            {
                case SoundEffectType.FacebookAlert:
                    return "Facebook Alert";
                case SoundEffectType.FacebookPop:
                    return "Facebook Pop";
                default:
                    return "Standard Notification";
            }
        }
    }
}
