using BeatSaberMarkupLanguage.Components;
using IPA.Loader;
using SiraUtil.Zenject;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using SiraUtil.Logging;
using Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VRUIControls;
using Zenject;

namespace ReProcessor.UI
{
    internal class ButtonManager : IInitializable, IDisposable
    {
        private ClickableImage? _image;
        public event Action? WasClicked;

        private readonly Assembly _assembly;
        private readonly SiraLog _log;
        private readonly DiContainer _container;
        private readonly TimeTweeningManager _tweeningManager;

        private readonly LevelSelectionNavigationController _levelSelectionNavigationController;
        //private static readonly Color _emptyColor = new Color(0.15f, 0f, 0f, 1f);

        public Color? DefaultColor
        {
            get
            {
                if (_image != null)
                {
                    return _image.DefaultColor;
                }

                return null;
            }
            set
            {
                if (_image != null && value.HasValue)
                {
                    Color oldColor = _image.color;
                    _tweeningManager.KillAllTweens(_image);
                    var tween = new FloatTween(0f, 1f, val => _image!.color = Color.Lerp(oldColor, value.Value, val), 1f, EaseType.InOutSine);
                    _tweeningManager.AddTween(tween, _image);
                    tween.onCompleted = delegate { _image!.DefaultColor = value.Value; };
                }
            }
        }

        public ButtonManager(DiContainer container, UBinder<Plugin, PluginMetadata> metadataBinder, TimeTweeningManager tweeningManager,
            LevelSelectionNavigationController levelSelectionNavigationController, SiraLog log)
        {
            _container = container;
            _tweeningManager = tweeningManager;
            _assembly = metadataBinder.Value.Assembly;
            _levelSelectionNavigationController = levelSelectionNavigationController;
            _log = log;
        }

        public void Initialize()
        {
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            string variant = "2";
            UnityEngine.Random.InitState(DateTime.UtcNow.Second);
            int rng = UnityEngine.Random.Range(3, 4);
            if ((rng.Equals(3)) && (DateTime.UtcNow.Month.Equals(6)))
                variant = "3";
            _image = CreateImage();
            using var ms = new MemoryStream();
            using (var mrs = _assembly.GetManifestResourceStream($"ReProcessor.Resources.MenuButtonImages.untitled{variant}.png")!)
            {
                await mrs.CopyToAsync(ms);
            }

            _image.OnClickEvent += Clicked;
            _image.sprite = BeatSaberMarkupLanguage.Utilities.LoadSpriteRaw(ms.ToArray());
            _image.sprite.texture.wrapMode = TextureWrapMode.Clamp;
            _log.Debug($"Button Init'd");
        }

        public void Dispose()
        {
            if (_image != null)
                _image.OnClickEvent -= Clicked;
        }

        private void Clicked(PointerEventData _)
        {
            _log.Debug($"Button clicked");
            WasClicked?.Invoke();
            _log.Debug($"Button event fired");
        }

        private ClickableImage CreateImage()
        {
            GameObject gameObject = new GameObject("ReProcessorButton");
            ClickableImage image = gameObject.AddComponent<ClickableImage>();
            image.material = BeatSaberMarkupLanguage.Utilities.ImageResources.NoGlowMat;

            image.rectTransform.SetParent(_levelSelectionNavigationController.transform);
            image.rectTransform.localPosition = new Vector3(79f, 18f, 0f);
            image.rectTransform.localScale = new Vector3(.3f, .3f, .3f);
            image.rectTransform.sizeDelta = new Vector2(20f, 20f);
            gameObject.AddComponent<LayoutElement>();

            var canvas = gameObject.AddComponent<Canvas>();
            canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1 
                                               | AdditionalCanvasShaderChannels.TexCoord2
                                               | AdditionalCanvasShaderChannels.Tangent
                                               | AdditionalCanvasShaderChannels.Normal;
            _container.InstantiateComponent<VRGraphicRaycaster>(gameObject);

            return image;
        }
    }
}