namespace BasicTwitchSoundPlayer.SoundDatabaseEditor
{
    public static class SupportedFileFormats
    {
        private static string[] privFilter = new string[] {"Wave Files (*.wav)|*.wav",
                        "MPEG-3 Layer (*.mp3)|*.mp3",
                        "Ogg Vorbis (*.ogg)|*.ogg",
                        "All Supported Formats|*.ogg;*.mp3;*.wav"
        };
        public static string Filter = string.Join("|", privFilter);
        public static int LastIndex = privFilter.Length;
    }
}
