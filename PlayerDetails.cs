namespace C17_Ex02
{
    class PlayerDetails
    {
        public string name;

        public string Name
        {
            get => name;
            set => name = value;
        }

        private byte points = 0;

        public byte Points
        {
            get => points;
            set => points = value;
        }

        public char playerSymbol = ' ';

        public char PlayerSymbol
        {
            get => playerSymbol;
            set => playerSymbol = value;
        }
    }
}
