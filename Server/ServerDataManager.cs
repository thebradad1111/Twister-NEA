﻿namespace Server
{
    public class ServerDataManager
    {
        private ServerDataManager()
        {
            
        }

        public static ServerDataManager Instance { get; } = new ServerDataManager();

        public Character character1 = new Character(1);
        public Character character2 = new Character(2);

        public bool CharactersCollided = false;
    }
}