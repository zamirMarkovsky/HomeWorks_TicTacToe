namespace C17_Ex02
{
    class PlayerDetails
    {
        public string m_Name;
        public byte m_Points;
        public char m_PlayerSymbol;

        public PlayerDetails()
        {
            m_Name = string.Empty;
            m_Points = 0;
            m_PlayerSymbol = ' ';
        }

        public string Name
        {
            get => m_Name;
            set => m_Name = value;
        }
      
        public byte Points
        {
            get => m_Points;
            set => m_Points = value;
        }
        
        public char PlayerSymbol
        {
            get => m_PlayerSymbol;
            set => m_PlayerSymbol = value;
        }
    }
}
