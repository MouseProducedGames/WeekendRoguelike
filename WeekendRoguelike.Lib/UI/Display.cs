using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeekendRoguelike.Mob.Character;
using WeekendRoguelike.Mob.UI;

namespace WeekendRoguelike.UI
{
    public abstract class Display
    {
        #region Private Fields

        private static Display instance;
        private static object lockObject = new object();

        private CharacterDisplayFactory characterDisplayFactory;

        #endregion Private Fields

        #region Public Interfaces

        public interface ICharacterGraphicsWrapper : IGraphicsWrapper
        {
            #region Public Methods

            void Update(CharacterData forCharacter);

            #endregion Public Methods
        }

        public interface IGraphicsWrapper
        {
            #region Public Methods

            void Draw();

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

        public ICharacterGraphicsWrapper CreateGraphicsWrapper(CharacterData forCharacter)
        {
            return CharacterDisplayFactory.Create(forCharacter);
        }

        public ICharacterGraphicsWrapper CreateGraphicsWrapper(string name)
        {
            return CharacterDisplayFactory.Create(name);
        }

        public abstract Listbox CreateListbox();

        #endregion Public Methods
    }

    public abstract class Display<T> : Display
    {
    }
}
