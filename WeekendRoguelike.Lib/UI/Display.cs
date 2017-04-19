using System;
using WeekendRoguelike.MapSystem;
using WeekendRoguelike.MapSystem.UI;
using WeekendRoguelike.CharacterSystem.Base;
using WeekendRoguelike.CharacterSystem.UI;

namespace WeekendRoguelike.UI
{
    public abstract class Display
    {
        #region Private Fields

        private static Display instance;
        private static object lockObject = new object();

        private CharacterDisplayFactory characterDisplayFactory;
        private MapDisplayFactory mapDisplayFactory;

        #endregion Private Fields

        #region Public Interfaces

        public interface ICharacterGraphicsWrapper : IGraphicsWrapper
        {
            #region Public Methods

            void Update(Character forCharacter);

            #endregion Public Methods
        }

        public interface IGraphicsWrapper
        {
            #region Public Methods

            void Draw(Character viewpointCharacter);

            #endregion Public Methods
        }

        public interface IMapGraphicsWrapper : IGraphicsWrapper
        {
            #region Public Methods

            void Update(Map forMap);

            #endregion Public Methods
        }

        #endregion Public Interfaces

        #region Public Properties

        public static Display Instance
        {
            get => instance;
        }

        #endregion Public Properties

        #region Protected Properties

        protected CharacterDisplayFactory CharacterDisplayFactory { get => characterDisplayFactory; set => characterDisplayFactory = value; }

        protected MapDisplayFactory MapDisplayFactory { get => mapDisplayFactory; set => mapDisplayFactory = value; }

        #endregion Protected Properties

        #region Public Methods

        public static T GetInstanceAs<T>()
            where T : Display
        {
            return instance as T;
        }

        public static void SetInstance(Display newInstance)
        {
            if (instance != null)
                throw new InvalidOperationException("Instance already exists.");
            lock (lockObject)
            {
                if (instance != null)
                    throw new InvalidOperationException("Instance already exists.");
                instance = newInstance;
            }
        }

        public abstract IGraphicsWrapper CreateGraphicsWrapper();

        public ICharacterGraphicsWrapper CreateGraphicsWrapper(Character forCharacter)
        {
            return CharacterDisplayFactory.Create(forCharacter);
        }

        public IMapGraphicsWrapper CreateGraphicsWrapper(Map forMap)
        {
            return MapDisplayFactory.Create(forMap);
        }

        public ICharacterGraphicsWrapper CreateGraphicsWrapper(string name)
        {
            return CharacterDisplayFactory.Create(name);
        }

        public abstract Listbox CreateListbox();

        public abstract void Update();

        #endregion Public Methods
    }

    public abstract class Display<T> : Display
    {
    }
}
