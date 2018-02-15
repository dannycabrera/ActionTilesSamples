using Android.App;
using Android.Widget;
using Android.OS;
using ActionComponents;
using UIKit;
using System;

namespace AndroidActionTiles
{
	[Activity(Label = "AndroidActionTiles", Theme = "@android:style/Theme.NoTitleBar", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Configure tiles
			var tileController = FindViewById<ACTileController>(Resource.Id.tileController);
			tileController.title = "Home Controller";
			tileController.navigationBar.ShowNavigationBar(true);
			tileController.scrollDirection = ACTileControllerScrollDirection.Vertical;

			// Stop the tile controller from updating while we populate it
			tileController.suspendUpdates = true;

			// Display type
			var standardTiles = true;

			if (standardTiles) {
				// Display standard tiles
				tileController.appearance.backgroundImage = UIImage.FromResources(Resources, Resource.Drawable.Linen);

				// Add and configure the first group
				var group1 = tileController.AddGroup(ACTileGroupType.ExpandingGroup, "Group One", "This is a sample group.");
				group1.appearance.hasBackground = false;
				group1.autoFitTiles = true;

				group1.AddTile(ACTileStyle.Default, ACTileSize.Single, "Today", "", "", Resources, Resource.Drawable.CalendarDay);
				group1.AddTile(ACTileStyle.Default, ACTileSize.DoubleVertical, "Crop", "", "", Resources, Resource.Drawable.Crop);
				group1.AddTile(ACTileStyle.BigPicture, ACTileSize.DoubleHorizontal, "Last Image", "", "", Resources, Resource.Drawable.Arrow).appearance.titleColor = ACColor.White;
				group1.AddTile(ACTileStyle.DescriptionBlock, ACTileSize.Quad, "Message", "Welcome!", "We hope that you are enjoying your ActionTile View. Look around and see what all I can do!", Resources, Resource.Drawable.OpenMail);
				group1.AddTile(ACTileStyle.Default, ACTileSize.Single, "Map", "", "", Resources, Resource.Drawable.Marker);
				group1.AddTile(ACTileStyle.TopTitle, ACTileSize.Single, "Locked", "", "", Resources, Resource.Drawable.Lock);
				group1.AddTile(ACTileStyle.CornerIcon, ACTileSize.Single, "Images", "32", "", Resources, Resource.Drawable.FilmRoll);
				group1.AddTile(ACTileStyle.Default, ACTileSize.Single, "Pasteboard", "", "", Resources, Resource.Drawable.Paste);

				// Randomily assign a purple color to the tiles in this group with the
				// given brightness range
				group1.ChromaKeyTiles(ACColor.Purple, 50, 250);

				// Assign a live update action to this group
				group1.liveUpdateAction = new ACTileLiveUpdateGroupChromaKey(group1, ACColor.Purple, 50, 250);

				// Add and configure the second group
				var group2 = tileController.AddGroup(ACTileGroupType.ExpandingGroup, "Group Two", "This group includes several live update features.");
				group2.appearance.hasBackground = false;
				group2.autoFitTiles = true;

				// Add tiles to second group
				var photos = group2.AddTile(ACTileStyle.Default, ACTileSize.Quad, "Photos", "", "", Resources, Resource.Drawable.ManWithChild);
				group2.AddTile(ACTileStyle.CornerIcon, ACTileSize.DoubleHorizontal, "Flight Arrives", "2:00pm", "", Resources, Resource.Drawable.Airplane);
				group2.AddTile(ACTileStyle.CornerIcon, ACTileSize.Single, "Cards", "", "", Resources, Resource.Drawable.ATMCard);
				group2.AddTile(ACTileStyle.Default, ACTileSize.DoubleVertical, "Briefcase", "", "", Resources, Resource.Drawable.Breifcase);
				var wakeup = group2.AddTile(ACTileStyle.Default, ACTileSize.DoubleHorizontal, "Wake-up", "6:30am", "", Resources, Resource.Drawable.Brightness);
				group2.AddTile(ACTileStyle.Default, ACTileSize.Single, "This Month", "", "", Resources, Resource.Drawable.CalendarMonth);
				var chat = group2.AddTile(ACTileStyle.Default, ACTileSize.Single, "Chat", "4", "", Resources, Resource.Drawable.Chat);
				var mail = group2.AddTile(ACTileStyle.Default, ACTileSize.DoubleHorizontal, "New Messages", "12", "", Resources, Resource.Drawable.ClosedMail);
				group2.AddTile(ACTileStyle.Default, ACTileSize.Single, "Portfolio", "", "", Resources, Resource.Drawable.Portfolio);
				group2.AddTile(ACTileStyle.Default, ACTileSize.DoubleVertical, "Files", "142", "", Resources, Resource.Drawable.Folder);
				group2.AddTile(ACTileStyle.Default, ACTileSize.DoubleHorizontal, "System Settings", "", "", Resources, Resource.Drawable.Gear);
				group2.AddTile(ACTileStyle.BigPicture, ACTileSize.Quad, "The Bay Bridge", "", "", Resources, Resource.Drawable.Bridge);
				group2.AddTile(ACTileStyle.Default, ACTileSize.Single, "Encrypt", "", "", Resources, Resource.Drawable.Key);
				group2.AddTile(ACTileStyle.BigPicture, ACTileSize.Single, "News", "", "", Resources, Resource.Drawable.Fountain);
				var systemState = group2.AddTile(ACTileStyle.DescriptionBlock, ACTileSize.Quad, "Links", "System Offline", "No remote links are currently active on this device. Tap tile to update...", Resources, Resource.Drawable.Link);
				group2.AddTile(ACTileStyle.Default, ACTileSize.Single, "You are here", "", "", Resources, Resource.Drawable.Marker);
				group2.AddTile(ACTileStyle.Default, ACTileSize.Single, "Recording", "", "", Resources, Resource.Drawable.Microphone);

				// Attach live update events to group 2 tiles
				photos.liveUpdateAction = new ACTileLiveUpdateTileImages(photos, new UIImage[] {
					UIImage.FromResources(Resources, Resource.Drawable.Arrow),
					UIImage.FromResources(Resources, Resource.Drawable.Boat),
					UIImage.FromResources(Resources, Resource.Drawable.Bridge),
					UIImage.FromResources(Resources, Resource.Drawable.Fountain),
					UIImage.FromResources(Resources, Resource.Drawable.ManWithChild)
				});
				mail.liveUpdateAction = new ACTileLiveUpdateTileImages(mail, new UIImage[] {
					UIImage.FromResources(Resources, Resource.Drawable.OpenMail),
					UIImage.FromResources(Resources, Resource.Drawable.ClosedMail) 
				});
				wakeup.liveUpdateAction = new ACTileLiveUpdateTileColor(wakeup, new ACColor[] {
					ACColor.Red,
					ACColor.ActionYellowOrange
				});

				group2.liveUpdateAction = new ACTileLiveUpdateGroupColorRandom(group2, new ACColor[] {
					ACColor.ActionBrightOrange,
					ACColor.ActionRedOrange,
					ACColor.Red,
					ACColor.ActionBrickRed
				});

				// Add multiple live updates to a given tile
				var sequence = new ACTileLiveUpdateTileSequence(systemState);
				sequence.Add(new ACTileLiveUpdateTileText(systemState, new string[] { "System Test", "System Offline" }, new string[] {
					"Testing...",
					"Links"
				}, new string[] {
					"Runing connectivity tests on device. Please standby...",
					"No remote links are currently active on this device. Tap tile to update..."
				}));
				sequence.Add(new ACTileLiveUpdateTileChromaKey(systemState, ACColor.Purple, 100, 250));

				systemState.liveUpdateAction = sequence;

				// Add and configure the third group
				var group3 = tileController.AddGroup(ACTileGroupType.ExpandingGroup, "Group Three", "");
				group3.autoFitTiles = true;

				// Add tiles to third group
				group3.AddTile(ACTileStyle.Default, ACTileSize.Quad, "Airplane Mode", "OFF", "", Resources, Resource.Drawable.Airplane);
				group3.AddTile(ACTileStyle.Default, ACTileSize.DoubleVertical, "Let's Chat", "", "", Resources, Resource.Drawable.Chat);
				var cloud = group3.AddTile(ACTileStyle.DescriptionBlock, ACTileSize.DoubleHorizontal, "Cloud", "", "There are 2 new files in your shared cloud", Resources, Resource.Drawable.Cloud);
				group3.AddTile(ACTileStyle.Default, ACTileSize.Quad, "On The Air", "", "", Resources, Resource.Drawable.RadioTower);
				group3.AddTile(ACTileStyle.Default, ACTileSize.Single, "Tags", "", "", Resources, Resource.Drawable.Tag);
				group3.AddTile(ACTileStyle.Default, ACTileSize.Single, "Wallet", "", "", Resources, Resource.Drawable.Wallet);
				group3.AddTile(ACTileStyle.Default, ACTileSize.DoubleVertical, "TV", "", "", Resources, Resource.Drawable.TV);
				group3.AddTile(ACTileStyle.Default, ACTileSize.Single, "Movies", "", "", Resources, Resource.Drawable.Movie);
				group3.AddTile(ACTileStyle.Default, ACTileSize.Single, "Microphone", "", "", Resources, Resource.Drawable.Microphone);
				group3.AddTile(ACTileStyle.CornerIcon, ACTileSize.Quad, "Current Time", "12:30pm", "", Resources, Resource.Drawable.Clock);
				group3.AddTile(ACTileStyle.Default, ACTileSize.DoubleHorizontal, "Contrast", "", "", Resources, Resource.Drawable.Contrast);
				group3.AddTile(ACTileStyle.Default, ACTileSize.DoubleHorizontal, "System Check-Up", "", "", Resources, Resource.Drawable.Checkmark);
				group3.AddTile(ACTileStyle.Default, ACTileSize.DoubleHorizontal, "This Week", "", "", Resources, Resource.Drawable.CalendarWeek);
				var pictureStream = group3.AddTile(ACTileStyle.BigPicture, ACTileSize.Quad, "PictureStream", "42", "", Resources, Resource.Drawable.Boat);
				group3.AddTile(ACTileStyle.Default, ACTileSize.Single, "Key", "", "", Resources, Resource.Drawable.Key);
				group3.AddTile(ACTileStyle.Default, ACTileSize.DoubleHorizontal, "Music", "", "", Resources, Resource.Drawable.Music);

				// Add live updates to group
				pictureStream.liveUpdateAction = new ACTileLiveUpdateTileImages(pictureStream, new UIImage[] { 
					UIImage.FromResources(Resources, Resource.Drawable.ManWithChild),
					UIImage.FromResources(Resources, Resource.Drawable.Bridge),
					UIImage.FromResources(Resources, Resource.Drawable.Arrow),
					UIImage.FromResources(Resources, Resource.Drawable.Fountain),
					UIImage.FromResources(Resources, Resource.Drawable.Boat) });
				group3.liveUpdateAction = new ACTileLiveUpdateGroupColor(group3, new ACColor[] {
					ACColor.ActionBrickRed,
					ACColor.ActionCyan,
					ACColor.ActionDustyRose,
					ACColor.ActionGrape,
					ACColor.ActionGreenTea,
					ACColor.ActionLavendar,
					ACColor.ActionNavyBlue,
					ACColor.ActionOrange,
					ACColor.ActionPink,
					ACColor.ActionPinkGrapefruit,
					ACColor.ActionRedOrange,
					ACColor.ActionSkyBlue,
					ACColor.ActionTeal,
					ACColor.ActionYellow,
					ACColor.ActionYellowGreen,
					ACColor.ActionYellowOrange
				});

				// Randomily assign a green color to the tiles in this group with the
				// given brightness range
				group3.ChromaKeyTiles(ACColor.ActionYellowGreen, 50, 250);

				// Add and configure the fourth group
				var group4 = tileController.AddGroup(ACTileGroupType.ExpandingGroup, "Group Four", "");
				group4.autoFitTiles = true;

				// Add tiles to fourth group
				group4.AddTile(ACTileStyle.Default, ACTileSize.Single, "Monitor", "", "", Resources, Resource.Drawable.Widescreen);
				group4.AddTile(ACTileStyle.Default, ACTileSize.DoubleVertical, "Desktop", "", "", Resources, Resource.Drawable.Arrow);
				group4.AddTile(ACTileStyle.DescriptionBlock, ACTileSize.DoubleHorizontal, "System Tools", "", "It's been 4 days since your last scan.", Resources, Resource.Drawable.Tools);
				group4.AddTile(ACTileStyle.BigPicture, ACTileSize.Quad, "Feeds", "15", "", Resources, Resource.Drawable.Fountain);
				group4.AddTile(ACTileStyle.Default, ACTileSize.Single, "locate", "", "", Resources, Resource.Drawable.Scanner);
				group4.AddTile(ACTileStyle.BigPicture, ACTileSize.Single, "", "", "", Resources, Resource.Drawable.Boat);
				group4.AddTile(ACTileStyle.Default, ACTileSize.Single, "Radio", "", "", Resources, Resource.Drawable.Radio);
				group4.AddTile(ACTileStyle.Default, ACTileSize.Single, "Labels", "", "", Resources, Resource.Drawable.Tag);
				group4.AddTile(ACTileStyle.Default, ACTileSize.Single, "Password", "", "", Resources, Resource.Drawable.Key);

				// Randomily assign a green color to the tiles in this group with the
				// given brightness range
				group4.ChromaKeyTiles(ACColor.ActionSkyBlue, 50, 250);
			} else {
				// Display as HomeKit style
				tileController.appearance.backgroundImage = UIImage.FromResources(Resources, Resource.Drawable.Agreement);

				// Style tiles
				var sceneSize = 4;
				tileController.appearance.cellSize = 50f;
				tileController.tileAppearance.titleSize = 14f;
				tileController.tileAppearance.subtitleColor = ACColor.FromRGB(100, 100, 100);
				tileController.tileAppearance.subtitleSize = 12f;

				// Add new group
				var scenes = tileController.AddGroup(ACTileGroupType.ExpandingGroup, "Favorite Scenes", "");

				scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Evening", "", "", Resources, Resource.Drawable.House);
				scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Go to Bed", "", "", Resources, Resource.Drawable.House);
				scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Night", "", "", Resources, Resource.Drawable.House);
				scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Start Work", "", "", Resources, Resource.Drawable.House);
				scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Sunset", "", "", Resources, Resource.Drawable.House);
				scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Wake Up", "", "", Resources, Resource.Drawable.House);

				// Add new group
				var accessories = tileController.AddGroup(ACTileGroupType.ExpandingGroup, "Favorite Accessories", "");

				accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Kitchen Eve Door", "No Response", "", Resources, Resource.Drawable.House);
				accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Patio Eve Humidity", "No Response", "", Resources, Resource.Drawable.House);
				accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Patio Eve Temp", "No Response", "", Resources, Resource.Drawable.House);
				accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Bedroom", "Off", "", Resources, Resource.Drawable.LightOff);
				accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Dining Room Left", "Off", "", Resources, Resource.Drawable.LightOff);
				accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Dining Room Right", "Off", "", Resources, Resource.Drawable.LightOff);
				accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Bloom", "Off", "", Resources, Resource.Drawable.LightOff);
				accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Iris", "Off", "", Resources, Resource.Drawable.LightOff);
				accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Lightstrip", "Off", "", Resources, Resource.Drawable.LightOff);
				accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Table Lamp", "Off", "", Resources, Resource.Drawable.LightOff);
				accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Wicker Bottom", "Off", "", Resources, Resource.Drawable.LightOff);
				accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Wicker Top", "Off", "", Resources, Resource.Drawable.LightOff);
				accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Office Desk", "Off", "", Resources, Resource.Drawable.LightOff);
				accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Office Side Table", "Off", "", Resources, Resource.Drawable.LightOff);

				// Wire-up accessories events
				accessories.TileTouched += (group, tile) => {
					switch (tile.subtitle)
					{
						case "Off":
							tile.subtitle = "On";
							if (tile.title == "Bedroom")
							{
								// Simulate a light color
								tile.ChromaKeyTile(ACColor.Purple, 50, 250);
							}
							else
							{
								// Just set to white
								tile.appearance.background = ACColor.White;
							}
							tile.icon = UIImage.FromResources(Resources, Resource.Drawable.LightOn);
							break;
						case "On":
							tile.subtitle = "Off";
							tile.appearance.background = ACColor.FromRGBA(213, 213, 213, 240);
							tile.icon = UIImage.FromResources(Resources, Resource.Drawable.LightOff);
							break;
					}
				};
			}

			// Allow update and automatically refresh the screen
			tileController.suspendUpdates = false;

			// Tell the controller to automatically update itself
			tileController.liveUpdate = standardTiles;

			// Display touched tile
			tileController.TileTouched += (group, tile) => {
				Console.WriteLine("Touched tile {0} in group {1}", tile.title, group.title);
			};
		}
	}
}

