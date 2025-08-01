using System;
using System.Collections;
using GyeMong.EventSystem.Controller;
using GyeMong.InputSystem;
using UnityEngine;
using GyeMong.SoundSystem;

namespace GyeMong.EventSystem.Event.Chat
{
    public abstract class ChatEvent : Event
    {
    }

    public class OpenChatEvent : ChatEvent
    {
        public override IEnumerator Execute(EventObject eventObject = null)
        {
            InputManager.Instance.SetActionState(false);
            return ChatController.Open();
        }
    }

    public class CloseChatEvent : ChatEvent
    {
        public override IEnumerator Execute(EventObject eventObject = null)
        {
            InputManager.Instance.SetActionState(true);
            ChatController.Close();
            yield return null;
            
        }
    }

    [Serializable]
    public class ShowMessages : ChatEvent
    {
        [SerializeField] private MultiChatMessageData chatData;

        [SerializeField] float autoSkipTime = 3f;
        float soundDelay = 0.2f;
        SoundObject _soundObject;

        public ShowMessages(MultiChatMessageData data, float autoSkip = 3f)
        {
            chatData = data;
            autoSkipTime = autoSkip;
        }

        public override IEnumerator Execute(EventObject eventObject = null)
        {
            foreach (var chat in chatData.chatMessages)
            {
                _soundObject = Sound.Play("EFFECT_Chat_Sound", true);
                yield return new WaitForSeconds(soundDelay);
                Sound.Stop(_soundObject);

                ChatController.SetBackgroundImage(ChatController.GetBackgroundImageSprite(chat.backgroundImage));
                ChatController.SetChatImage(ChatController.GetChatImageSprite(chat.chatImage));

                if (chat.chatImage != ChatImage.nothing)
                {
                    yield return new WaitForSeconds(2f);
                }

                yield return ChatController.MultipleChat(chat, autoSkipTime);
            }

            ChatController.SetBackgroundImage(null);
            ChatController.SetChatImage(null);
            ChatController.SetCharacterImage(null, true);
            ChatController.SetCharacterImage(null, false);
        }
    }

    [Serializable]
    public class SpeechBubbleChatEvent : ChatEvent
    {
        [SerializeField]
        GameObject NPC;
        [SerializeField]
        string message;
        [SerializeField]
        float destroyDelay;
        public override IEnumerator Execute(EventObject eventObject)
        {
            return SceneContext.EffectManager.GetChatController().ShowSpeechBubbleChat(NPC, message, destroyDelay);
        }
    }
}