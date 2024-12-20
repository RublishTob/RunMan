using System;
using System.Threading;
using System.Threading.Tasks;
using ReadyPlayerMe.AvatarCreator;
using ReadyPlayerMe.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using TaskExtensions = ReadyPlayerMe.AvatarCreator.TaskExtensions;

namespace ReadyPlayerMe.Samples.AvatarCreatorElements
{
    public class AvatarHandler : MonoBehaviour
    {
        [SerializeField] private RuntimeAnimatorController animationController;

        public UnityEvent<AvatarProperties> OnAvatarLoaded;
        public UnityEvent OnAvatarLoading;

        private AvatarManager avatarManager;
        private readonly CancellationTokenSource cancellationTokenSource = new();
        private GameObject avatar;
        public AvatarProperties ActiveAvatarProperties { get; private set; }

        private void Awake()
        {
           avatarManager = new AvatarManager(token: cancellationTokenSource.Token);
        }

        public async Task LoadAvatar(string avatarId)
        {
            await LoadAvatarWithId(avatarId);
        }

        public async Task SelectAsset(IAssetData assetData)
        {
            OnAvatarLoading?.Invoke();
            var newAvatar = await avatarManager.UpdateAsset(assetData.AssetType, assetData.Id);
            SetupLoadedAvatar(newAvatar, ActiveAvatarProperties);
        }

        public async Task SaveActiveAvatar()
        {
            await avatarManager.Save();
            AuthManager.StoreLastModifiedAvatar(avatarManager.AvatarId);
        }

        public async Task CreateNewAvatarFromTemplate()
        {
            await CreateTemplateAvatar();
        }

        public async void LoadPreviousOrCreateNewAvatar()
        {
            var session = AuthManager.UserSession;
            await TaskExtensions.HandleCancellation(string.IsNullOrEmpty(session.LastModifiedAvatarId) ? CreateTemplateAvatar() : LoadAvatarWithId(session.LastModifiedAvatarId));
        }



        private async Task<AvatarProperties> LoadAvatarWithId(string avatarId)
        {
            OnAvatarLoading?.Invoke();
            var newAvatar = await avatarManager.GetAvatar(avatarId);
            var newAvatarProperties = await avatarManager.GetAvatarProperties(avatarId);

            SetupLoadedAvatar(newAvatar, newAvatarProperties);
            AuthManager.StoreLastModifiedAvatar(avatarId);

            return newAvatarProperties;

        }

        /// <summary>
        ///     Creates an avatar from a template and sets its initial properties.
        /// </summary>
        /// <returns>The properties of the created avatar.</returns>
        /// 

        public async Task <GameObject> LoadAvatarFromSelfie(Texture2D photo)
        {
            if (photo == null)
            {
                Debug.Log("NO PHOTO");
                return null;
            }
            var startTime = Time.time;

            GameObject avatar;

            var avatarProps = new AvatarProperties();
            avatarProps.Id = "67529f7c9ae073e982e0934c";
            avatarProps.Partner = "runtemp";
            avatarProps.BodyType = BodyType.FullBody;
            avatarProps.Base64Image = Texture2DToBase64(photo);
            avatarProps.Assets = new();

            var avatarResponse = await avatarManager.CreateAvatarAsync(avatarProps);
            avatar = avatarResponse.AvatarObject;

            if (avatar == null)
            {
                Debug.Log("CANT LOAD AVATAR");
            }
            SetupLoadedAvatar(avatar, avatarResponse.Properties);

            return avatar;
        }
        public string Texture2DToBase64(Texture2D texture)
        {
            byte[] imageData = texture.EncodeToPNG();
            return Convert.ToBase64String(imageData);
        }

        private async Task<AvatarProperties> CreateTemplateAvatar()
        {
            OnAvatarLoading?.Invoke();
            var avatarTemplateFetcher = new AvatarTemplateFetcher(cancellationTokenSource.Token);
            var templates = await avatarTemplateFetcher.GetTemplates();
            var avatarTemplate = templates[1];
            return await LoadAvatarFromTemplate(avatarTemplate.Id);
        }

        public async Task<AvatarProperties> LoadAvatarFromTemplate(string templateId)
        {
            OnAvatarLoading?.Invoke();
            var templateAvatarResponse = await avatarManager.CreateAvatarFromTemplateAsync(templateId);

            SetupLoadedAvatar(templateAvatarResponse.AvatarObject, templateAvatarResponse.Properties);

            return templateAvatarResponse.Properties;
        }

        private void SetupLoadedAvatar(GameObject newAvatar, AvatarProperties newAvatarProperties)
        {
            if (avatar != null)
            {
                Destroy(avatar);
            }

            avatar = newAvatar;
            ActiveAvatarProperties = newAvatarProperties;

            SetupAvatar(newAvatarProperties);
            OnAvatarLoaded?.Invoke(newAvatarProperties);

        }

        /// <summary>
        ///     Sets additional elements and components on the created avatar, such as mouse rotation and animation controller.
        /// </summary>
        private void SetupAvatar(AvatarProperties newAvatarProperties)
        {
            //avatar.AddComponent<MouseRotationHandler>();
            //avatar.AddComponent<AvatarRotator>();
            var animator = avatar.GetComponent<Animator>();
            AvatarAnimationHelper.SetupAnimator(new AvatarMetadata
                { BodyType = newAvatarProperties.BodyType, OutfitGender = newAvatarProperties.Gender }, animator);
            animator.runtimeAnimatorController = animationController;
        }

        private void OnDestroy()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }

    }
}
