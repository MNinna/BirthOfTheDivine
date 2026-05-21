    using UnityEngine;

namespace Events
{
    public class GameEventManager : MonoBehaviour
    {
        // Singleton
        public static GameEventManager Instance { get; private set; }

        public InputEvents inputEvents;
        public UIEvents uiEvents;
        public SceneEvents sceneEvents;
        public ResourceEvents resourceEvents;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("Multiple instances of GameEventManager detected!");
                Destroy(gameObject);
            }
            Instance = this;
            
            Cursor.visible = false;

            // Initialize events
            inputEvents = new InputEvents();
            uiEvents = new UIEvents();
            sceneEvents = new SceneEvents();
            resourceEvents = new ResourceEvents();
        }
    }
}
