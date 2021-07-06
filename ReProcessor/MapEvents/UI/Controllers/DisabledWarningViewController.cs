using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SongCore;
using System.Reflection;
using UnityEngine;
using static BeatSaberMarkupLanguage.Components.CustomListTableData;

namespace ReProcessor.MapEvents.UI.Controllers
{
    public class DisabledWarningViewController : NotifiableSingleton<DisabledWarningViewController>
    {
        private StandardLevelDetailViewController standardLevel;


        internal static BS_Utils.Utilities.Config ModPrefs = new BS_Utils.Utilities.Config("SongCore/SongCore");

        //Currently selected song data
        public CustomPreviewBeatmapLevel level;
        public SongCore.Data.ExtraSongData songData;
        public SongCore.Data.ExtraSongData.DifficultyData diffData;
        public bool wipFolder;


        private bool _buttonGlow = true;
        [UIValue("button-glow")]
        public bool ButtonGlowColor
        {
            get => _buttonGlow;
            set
            {
                _buttonGlow = value;
                NotifyPropertyChanged();
            }
        }

        private bool buttonInteractable = true;
        [UIValue("button-interactable")]
        public bool ButtonInteractable
        {
            get => buttonInteractable;
            set
            {
                buttonInteractable = value;
                NotifyPropertyChanged();
            }
        }

        [UIComponent("info-button")]
#pragma warning disable 649 //assigned by BSML
        private Transform infoButtonTransform;
#pragma warning restore 649

        internal void Setup()
        {
            GetIcons();
            standardLevel = Resources.FindObjectsOfTypeAll<StandardLevelDetailViewController>().First();
            BSMLParser.instance.Parse(BeatSaberMarkupLanguage.Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "ReProcessor.MapEvents.UI.Views.DisabledWarning.bsml"), standardLevel.transform.Find("LevelDetail").gameObject, this);
            infoButtonTransform.localScale *= 0.7f;//no scale property in bsml as of now so manually scaling it
            // ReSharper disable once PossibleNullReferenceException
            (standardLevel.transform.Find("LevelDetail").Find("FavoriteToggle")?.transform as RectTransform).anchoredPosition = new Vector2(3, -2);
        }

        internal void GetIcons()
        {
            
        }

        [UIAction("button-click")]
        internal void ShowRequirements()
        {

        }
    }
}
