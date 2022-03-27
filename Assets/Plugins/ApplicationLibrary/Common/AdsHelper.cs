


/*namespace Alexplay.OilRush.Library.Common
{
    [DisallowMultipleComponent]
    public class AdsHelper : IService
    {
        [SerializeField] private float _interstitialCountdown = 130f;
        [SerializeField] private string _androidIronSourceId;
        [SerializeField] private string _iosIronSourceId;

        public event Action<bool> OnVideoStateChanged;
        public event Action<TimeSpan> OnAdVideoShowed;
        
        private const string LastAdVideoStartTimeKey = "AdsHelper.lastAdVidioStartTime";

        private bool _initialized;
        private Action _onVideoShown;
        private volatile ThreadHelper _threadHelper;
        private float _interstitialTimer;
        private VideoPlace _place;
        private bool _rewarded;
        string appID = "";
        private DateTime LastAdVideoStartTime
        {
            get
            {
                var timeString = PlayerPrefs.GetString(LastAdVideoStartTimeKey);  
                return DateTime.ParseExact(timeString, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }
            set => PlayerPrefs.SetString(LastAdVideoStartTimeKey, value.ToString(CultureInfo.InvariantCulture));
        }
        
        public override void Setup(MessengerKeys EventKey)
        {
            InitAds(GdprUtils.IsGdprAdvertisingAccepted()||!Context.GDPRGeoData);
            base.Setup(EventKey);
        }

        public void InitAds(bool accepted)
        {
            IronSource.Agent.setConsent(accepted);
            IronSource.Agent.setMetaData("do_not_sell",(!accepted).ToString());
           
          
            if (!_initialized)
            {
                  //Debug.Log("[AdsHelper] Start : ");

            IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
            IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent; 
            IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
            IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
            IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
            IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent; 
            IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
            IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;
            
            IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
            IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;        
            IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent; 
            IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent; 
            IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
            IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
            IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
            
            IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
            IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;        
            IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent; 
            IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent; 
            IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
            IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;
            
            IronSourceEvents.onImpressionSuccessEvent += ImpressionSuccessEvent;

#if UNITY_ANDROID
            IronSource.Agent.init (_androidIronSourceId, 
                IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);


#elif UNITY_IOS
            IronSource.Agent.init (_iosIronSourceId, 
                IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
#endif

            if (!PremPurchaseUtils.IsPurchased(PremPurchaseType.NO_ADS) && !PremPurchaseUtils.IsPurchased(PremPurchaseType.VIP))
            {
                IronSource.Agent.loadInterstitial();
                //IronSource.Agent.loadBanner(new IronSourceBannerSize(250, 40), IronSourceBannerPosition.BOTTOM);
            }

            IronSource.Agent.validateIntegration();
            StartCoroutine(InterstitialTimer());
            _initialized = true;
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
            Messenger.AddListener(MessengerKeys.BANK_SALES_COUNT_CHANGED, OnBankSalesCountChanged);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            IronSource.Agent.onApplicationPause(pauseStatus);
        }

        private void OnApplicationFocus(bool focus)
        {
            IronSource.Agent.onApplicationPause(!focus);
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }

        private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            _onVideoShown = null;
        }

        public void ShowBanner() => IronSource.Agent.displayBanner();
        public void HideBanner() => IronSource.Agent.hideBanner();

        public bool HasVideo()
        {

#if UNITY_EDITOR
            return true;
#else
            return IronSource.Agent.isRewardedVideoAvailable();
#endif
        }

        public void PlayVideo(Action callback, VideoPlace place)
        {
#if UNITY_EDITOR
            OnAdVideoShowed?.Invoke(TimeSpan.FromSeconds(15));
            callback.Invoke();
            _interstitialTimer = - 6 * 60 + _interstitialCountdown;
#else
                if (IronSource.Agent.isRewardedVideoAvailable())
                {
                    if (_threadHelper == null)
                        _threadHelper = GetComponent<ThreadHelper>();
                    AudioListener.volume = 0;
                    AnalyticsUtils.SendAdsEvent(AdsAnalyticsEventType.ADS_REWARDED_START, ParameterType.placement, place.ToString());
                    IronSource.Agent.showRewardedVideo();
                    Time.timeScale = 0;
                    LastAdVideoStartTime = DateTime.UtcNow;
                    _onVideoShown = callback;
                    _place = place;
                    _interstitialTimer = 0;
                }
                else
                {
                    if (Application.internetReachability == NetworkReachability.NotReachable)
                    {
                        AnalyticsUtils.SendAdsEvent(AdsAnalyticsEventType.ADS_REWARDED_FAILED, ParameterType.placement,place.ToString(),ParameterType.error,"internet_not_available");
                    }
                    else
                    {
                        AnalyticsUtils.SendAdsEvent(AdsAnalyticsEventType.ADS_REWARDED_FAILED, ParameterType.placement,place.ToString(),ParameterType.error,"video_not_available");
                    }
                }
#endif
        }

        public void ShowInterstitialIfNeed()
        {
            if(!PremPurchaseUtils.IsPurchased(PremPurchaseType.NO_ADS))
                if (_interstitialTimer >= _interstitialCountdown && IronSource.Agent.isInterstitialReady() && LocationUtils.GetVisitsCount(LocationType.COAST)>=1)
                {
                    IronSource.Agent.showInterstitial();
                    IronSource.Agent.loadInterstitial();
                    _interstitialTimer = 0;
                }
        }

        private IEnumerator InterstitialTimer()
        {
            while (enabled)
            {
                yield return new WaitForSeconds(_interstitialCountdown);
                _interstitialTimer += _interstitialCountdown;
            }
        }
        
        private void ImpressionSuccessEvent(IronSourceImpressionData impressionData) {
            if (impressionData != null)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param["ad_platform"] = "ironSource";
                param["adNetwork"] = impressionData.adNetwork;
                param["adUnit"] = impressionData.adUnit;
                param["instanceName"] = impressionData.instanceName;
                param["currency"] = "USD";
                param["value"] = impressionData.revenue.ToString();
                param["auctionId"] = impressionData.auctionId;
                param["lifetimeRevenue"] = impressionData.lifetimeRevenue.ToString();
                param["country"] = impressionData.country;
                param["ab"] = impressionData.ab;
                param["segmentName"] = impressionData.segmentName;
                param["placement"] = impressionData.placement;
                param["instanceId"] = impressionData.instanceId;
                param["precision"] = impressionData.precision;
                param["encryptedCPM"] = impressionData.encryptedCPM;
                param["conversionValue"] = impressionData.conversionValue.ToString();

                AppsFlyerSDK.AppsFlyer.sendEvent("ad_impression", param);
            }
        }

        private void RewardedVideoAdOpenedEvent()
        {

            //Debug.Log("[AdsHelper] RewardedVideoAdOpenedEvent : ");
        }
        
        private void RewardedVideoAdClosedEvent()
        {
            Time.timeScale = 1;
            AudioListener.volume = 1;
            OnAdVideoShowed?.Invoke(DateTime.UtcNow - LastAdVideoStartTime);

           // Debug.Log("[AdsHelper] RewardedVideoAdClosedEvent : ");
            CancelInvoke(nameof(CheckAdsClose));
            Invoke(nameof(CheckAdsClose),1);
        }

        private void CheckAdsClose()
        {
            if (_rewarded)
            {
                AnalyticsUtils.SendAdsEvent(AdsAnalyticsEventType.ADS_REWARDED_SHOW, ParameterType.placement, _place.ToString());
            }
            else
            {
                AnalyticsUtils.SendAdsEvent(AdsAnalyticsEventType.ADS_REWARDED_CLOSED, ParameterType.placement, _place.ToString());
            }
            _rewarded = false;
        }
        private void RewardedVideoAdRewardedEvent(IronSourcePlacement ironSourcePlacement) 
        {
            #region Monetization
            MonetizationUtils.AppendAmountOfWatchedRewardedAds();
            #endregion
            
            _rewarded = true;
            AudioListener.volume = 1;
            //Debug.Log("[AdsHelper] RewardedVideoAdRewardedEvent : " + ironSourcePlacement);
            _threadHelper.Enqueue(() =>
            {
                AdsAnalyzer.AppendSessionsRewarded();
                AnalyticsUtils.AppendRewardedCount();
                _onVideoShown?.Invoke();
                _onVideoShown = null;
                _interstitialTimer = 0;
            });
        }
        
        private void RewardedVideoAvailabilityChangedEvent(bool b) 
        {
            //Debug.Log("[AdsHelper] RewardedVideoAvailabilityChangedEvent : " + b);
            if (_threadHelper == null)
                _threadHelper = GetComponent<ThreadHelper>();
            
            _threadHelper.Enqueue(() => OnVideoStateChanged?.Invoke(b));
        }
        
        private void RewardedVideoAdStartedEvent() 
        {
            Time.timeScale = 0;
            AudioListener.volume = 0;
            LastAdVideoStartTime = DateTime.UtcNow;
            // Debug.Log("[AdsHelper] RewardedVideoAdStartedEvent : ");
        }
        
        private void RewardedVideoAdEndedEvent() 
        {
            Time.timeScale = 1;
            AudioListener.volume = 1;
            OnAdVideoShowed?.Invoke(DateTime.UtcNow - LastAdVideoStartTime);

            //Debug.Log("[AdsHelper] RewardedVideoAdEndedEvent : ");
        }
        

        
        private void RewardedVideoAdShowFailedEvent(IronSourceError ironSourceError) 
        {
            Time.timeScale = 1;
            OnAdVideoShowed?.Invoke(DateTime.UtcNow - LastAdVideoStartTime);
            AudioListener.volume = 1;
            AnalyticsUtils.SendAdsEvent(AdsAnalyticsEventType.ADS_REWARDED_FAILED, ParameterType.placement,_place.ToString(),ParameterType.error,ironSourceError.getDescription());
            //Debug.Log("[AdsHelper] RewardedVideoAdShowFailedEvent : " + ironSourceError);
           // Debug.Log("[AdsHelper] RewardedVideoAdShowFailedEvent : Description (" + ironSourceError.getDescription()+")");
        }
        private void RewardedVideoAdClickedEvent(IronSourcePlacement ironSourcePlacement) 
        {
            if (SessionUtils.GetDaysAfterInstallation() < 7)
            {
                Messenger.Broadcast(MessengerKeys.ADS_CLICKED_COUNT_CHANGED);
                SessionUtils.AppendAdsClicksInSevenDays();
            }

            //Debug.Log("[AdsHelper] RewardedVideoAdClickedEvent : ");
        }
        
        private void InterstitialAdReadyEvent() 
        {
            //Debug.Log("[AdsHelper] InterstitialAdReadyEvent : ");
        }
        private void InterstitialAdLoadFailedEvent(IronSourceError ironSourceError) 
        {
            //Debug.Log("[AdsHelper] InterstitialAdLoadFailedEvent : " + ironSourceError);
        }
        private void InterstitialAdShowSucceededEvent()
        {
            SessionUtils.AppendAdsShown();
            AnalyticsUtils.SendAdsEvent(AdsAnalyticsEventType.ADS_INTER_SHOW);
            #region Monetization
            MonetizationUtils.AppendAmountOfWatchedInterstitialAds();
            #endregion
            //Debug.Log("[AdsHelper] InterstitialAdShowSucceededEvent : ");
        }
        private void InterstitialAdShowFailedEvent(IronSourceError ironSourceError) 
        {
            AnalyticsUtils.SendAdsEvent(AdsAnalyticsEventType.ADS_INTER_FAILED, ParameterType.error,ironSourceError.getDescription());
            // Debug.Log("[AdsHelper] InterstitialAdShowFailedEvent : " + ironSourceError);
        }
        private void InterstitialAdClickedEvent() 
        {
            if(SessionUtils.GetDaysAfterInstallation() < 7)
                SessionUtils.AppendAdsClicksInSevenDays();
            //Debug.Log("[AdsHelper] InterstitialAdClickedEvent : ");
        }
        private void InterstitialAdOpenedEvent() 
        {
            AnalyticsUtils.SendAdsEvent(AdsAnalyticsEventType.ADS_INTER_START);
            //Debug.Log("[AdsHelper] InterstitialAdOpenedEvent : ");
        }
        
        private void InterstitialAdClosedEvent() 
        {
            #region Monetization
            MonetizationUtils.AppendAmountOfWatchedInterstitialAds();
            #endregion
            //Debug.Log("[AdsHelper] InterstitialAdClosedEvent : ");
            #region Monetization
            MonetizationUtils.AppendAmountOfSkippedInterstitialAds();
            #endregion
        }
        
        void BannerAdLoadedEvent() {
            IronSource.Agent.hideBanner();
        }

        void BannerAdLoadFailedEvent (IronSourceError error) {
        }
        void BannerAdClickedEvent () {
        }
        void BannerAdScreenPresentedEvent ()
        {
            string SceneName = SceneManager.GetActiveScene().name;
            if (SceneName.Equals("bank") || SceneName.Equals("mine")) IronSource.Agent.displayBanner();
            else IronSource.Agent.hideBanner();
        }
        void BannerAdScreenDismissedEvent() {
        }
        void BannerAdLeftApplicationEvent() {
        }
        
        private void OnBankSalesCountChanged(Bundle bundle)
        {
            if(GameStateUtils.GetBankSalesCount() % 3 == 0) ShowInterstitialIfNeed();
        }
    }
    
    public enum VideoPlace
    {
        none,
        
        PASSIVE_X2,
        AUCTION_CONTAINER,
        
        CHESTS_MINUS_GOLD_15MIN,
        CHESTS_MINUS_SILVER_15MIN,
        CHESTS_MINUS_BRONZE_15MIN,
        CHESTS_OPEN_GOLD,
        CHESTS_OPEN_SILVER,
        CHEST,
        
        BONUSES_X2,
        BONUSES_X3,
        BONUSES_AUTO,
        BONUSES_SPEED_UP,
        BONUSES,

        SMOKE_COMPACTOR,
        SMOKE_CONTAINER,

        BANK_SELL_X2,
        BANK_CONTRACT,
        
        FOREST_FLOOR,
        
        RACING_RESTART,
        RACING_RESET_OPPONENTS,
        
        CONTAINER_X2_HOME,
        CONTAINER_X2_DESERT,
        CONTAINER_X2_COAST,
        CONTAINER_X2_CITY,
        CONTAINER_X2_SEA,
        CONTAINER_X2_ORBIT,
        CONTAINER_X2_VOLCANO,
        CONTAINER_X2_GLACIER,
        CONTAINER_X2_CANYON,
        CONTAINER_X2_SAVANA,
        CONTAINER_X2_JUNGLI,
        CONTAINER_X2,
        
        STATISTICS_COLLECT_ALL,
        
        FACTORY_ALCHEMY_SPEED_UP,
        FACTORY_HEAP_SPEED_UP,
        
        LOCATIONS_LOCATION_OPEN_15MIN,
        LOCATIONS_LOCATION_CLEAR_15MIN,
        
        LABORATORY_RESEARCH_MINUS_15MIN,
        
        COMMON_MINUS_15_MIN,
        MAP_BALLOONS,
        BANK_MINIGAME,
        SPEED_UP_SQUID_GAME_MACHINE_UNBLOCK,
        INTERACTIVE_COLLECT_TRASH,
        INTERACTIVE_COLLECT_PASSIVE,
        GAME_BONUS_UFO,
        GAME_BONUS_AUTO,
        GAME_BONUS_MONEY,
        GAME_BONUS_DOUBLE,
        GAME_BONUS_ASK_HELP,
        DAILY_PRIZE,
    }
}*/
