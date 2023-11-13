using BepInEx;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using Utilla;

namespace helo
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        GameObject helocoopter;
        public GameObject forward;
        public GameObject right;
        public GameObject backward;
        public GameObject left;
        public GameObject up;
        public GameObject down;
        public GameObject leftturn;
        public GameObject rightturn;

        void Start()
        {
            /* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */

            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled*/

            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            var bundle = LoadAssetBundle("helo.assets.helo");
            helocoopter = bundle.LoadAsset<GameObject>("helo");
            helocoopter = GameObject.Instantiate(helocoopter);
            setup();
        }
        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }
        void setup()
        {    
            helocoopter.transform.localPosition = new Vector3 (-65.4993f, 21.6141f, - 82.7445f);
            up = helocoopter.transform.Find("Control panel/up key").gameObject;
            backward = helocoopter.transform.Find("Control panel/backward").gameObject;
            forward = helocoopter.transform.Find("Control panel/forward").gameObject;
            left = helocoopter.transform.Find("Control panel/left").gameObject;
            down = helocoopter.transform.Find("Control panel/down key").gameObject;
            right = helocoopter.transform.Find("Control panel/right").gameObject;
            rightturn = helocoopter.transform.Find("Control panel/right turn").gameObject;
            leftturn = helocoopter.transform.Find("Control panel/left turn").gameObject;

            up.AddComponent<Keys>().drone = helocoopter;
            backward.AddComponent<Keys>().drone = helocoopter;
            forward.AddComponent<Keys>().drone = helocoopter;
            left.AddComponent<Keys>().drone = helocoopter;
            right.AddComponent<Keys>().drone = helocoopter;
            rightturn.AddComponent<Keys>().drone = helocoopter;
            leftturn.AddComponent<Keys>().drone = helocoopter;
            down.AddComponent<Keys>().drone = helocoopter;
            up.layer = 18;
            down.layer = 18;
            backward.layer = 18;
            forward.layer = 18;
            left.layer = 18;
            right.layer = 18;
            rightturn.layer = 18;
            leftturn.layer = 18;
            helocoopter.SetActive(false);
        }

        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            /* Activate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = true;
            helocoopter.SetActive(true);
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            /* Deactivate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = false;
            helocoopter.SetActive(false);
        }
    }
}
