using System;
using UIKit;
using ActionComponents;

namespace iOSActionTiles
{
	public partial class FirstViewController : UIViewController
	{
		#region Constructors
		protected FirstViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
		#endregion

		#region Override Methods
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Configure the tile collection
			// Setup tile controller
			tileController.title = "Action Tiles";
			tileController.scrollDirection = ACTileControllerScrollDirection.Vertical;
			tileController.appearance.backgroundImage = UIImage.FromBundle("Linen.jpg");
			tileController.navigationBar.ShowNavigationBar(true);

			// Set default group appearances
			tileController.groupAppearance.roundTopLeftCorner = false;
			tileController.groupAppearance.roundTopRightCorner = false;
			tileController.groupAppearance.roundBottomLeftCorner = false;
			tileController.groupAppearance.roundBottomRightCorner = false;

			// Set default tile appearances
			tileController.tileAppearance.roundTopLeftCorner = false;
			tileController.tileAppearance.roundTopRightCorner = false;
			tileController.tileAppearance.roundBottomLeftCorner = false;
			tileController.tileAppearance.roundBottomRightCorner = false;

			// Suspend updates
			tileController.suspendUpdates = true;

			// Add new group
			var group1 = tileController.AddGroup(ACTileGroupType.ExpandingGroup, "Group One", "The tiles in this section automatically change color.");
			group1.appearance.hasBackground = false;
			group1.autoFitTiles = true;

			group1.AddTile(ACTileStyle.Default, ACTileSize.Single, "Today", "", "", "CalendarDay");
			group1.AddTile(ACTileStyle.Default, ACTileSize.DoubleVertical, "Crop", "", "", "Crop");
			group1.AddTile(ACTileStyle.BigPicture, ACTileSize.DoubleHorizontal, "Last Image", "", "", "Arrow.jpg").appearance.titleColor = ACColor.White;
			group1.AddTile(ACTileStyle.DescriptionBlock, ACTileSize.Quad, "Message", "Welcome!", "We hope that you are enjoying your ActionTile View. Look around and see what all I can do!", "OpenMail");
			group1.AddTile(ACTileStyle.Default, ACTileSize.Single, "Map", "", "", "Marker");
			group1.AddTile(ACTileStyle.TopTitle, ACTileSize.Single, "Locked", "", "", "Lock");
			group1.AddTile(ACTileStyle.CornerIcon, ACTileSize.Single, "Images", "32", "", "FilmRoll");
			group1.AddTile(ACTileStyle.Default, ACTileSize.Single, "Pasteboard", "", "", "PasteBoard");

			// Create a custom drawn tile in this group
			var customTile = group1.AddTile(ACTileStyle.CustomDrawn, ACTileSize.Single, "Picture", "", "", "Polaroid");

			// Respond to the draw event
			customTile.RequestCustomDraw += (tile, rect) => {

				//Fill with an image
				UIImage image = UIImage.FromBundle("Arrow.jpg");
				image.Draw(rect);

				//Request a standard bottom title bar be drawn
				tile.DrawBottomTitleBar(rect, true);
			};

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
			var photos = group2.AddTile(ACTileStyle.Default, ACTileSize.Quad, "Photos", "", "", "ManWithChild.jpg");
			group2.AddTile(ACTileStyle.CornerIcon, ACTileSize.DoubleHorizontal, "Flight Arrives", "2:00pm", "", "Airplane");
			group2.AddTile(ACTileStyle.CornerIcon, ACTileSize.Single, "Cards", "", "", "ATMCard");
			group2.AddTile(ACTileStyle.Default, ACTileSize.DoubleVertical, "Briefcase", "", "", "Briefcase");
			var wakeup = group2.AddTile(ACTileStyle.Default, ACTileSize.DoubleHorizontal, "Wake-up", "6:30am", "", "Brightness");
			group2.AddTile(ACTileStyle.Default, ACTileSize.Single, "This Month", "", "", "CalendarMonth");
			var chat = group2.AddTile(ACTileStyle.Default, ACTileSize.Single, "Chat", "4", "", "Chat");
			var mail = group2.AddTile(ACTileStyle.Default, ACTileSize.DoubleHorizontal, "New Messages", "12", "", "ClosedMail");
			group2.AddTile(ACTileStyle.Default, ACTileSize.Single, "Portfolio", "", "", "Portfolio");
			group2.AddTile(ACTileStyle.Default, ACTileSize.DoubleVertical, "Files", "142", "", "Folder");
			group2.AddTile(ACTileStyle.Default, ACTileSize.DoubleHorizontal, "System Settings", "", "", "Gear");
			group2.AddTile(ACTileStyle.BigPicture, ACTileSize.Quad, "The Bay Bridge", "", "", "Bridge.jpg");
			group2.AddTile(ACTileStyle.Default, ACTileSize.Single, "Encrypt", "", "", "Key");
			group2.AddTile(ACTileStyle.BigPicture, ACTileSize.Single, "News", "", "", "Fountain.jpg");
			var systemState = group2.AddTile(ACTileStyle.DescriptionBlock, ACTileSize.Quad, "Links", "System Offline", "No remote links are currently active on this device. Tap tile to update...", "Link");
			group2.AddTile(ACTileStyle.Default, ACTileSize.Single, "You are here", "", "", "Marker");
			group2.AddTile(ACTileStyle.Default, ACTileSize.Single, "Recording", "", "", "Microphone");

			// Attach live update events to group 2 tiles
			photos.liveUpdateAction = new ACTileLiveUpdateTileImages(photos, new string[] {
				"Arrow.jpg",
				"Boat.jpg",
				"Bridge.jpg",
				"Fountain.jpg",
				"ManWithChild.jpg"
			});
			mail.liveUpdateAction = new ACTileLiveUpdateTileImages(mail, new string[] { "OpenMail", "ClosedMail" });
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
			group3.AddTile(ACTileStyle.Default, ACTileSize.Quad, "Airplane Mode", "OFF", "", "Airplane");
			group3.AddTile(ACTileStyle.Default, ACTileSize.DoubleVertical, "Let's Chat", "", "", "Chat");
			var cloud = group3.AddTile(ACTileStyle.DescriptionBlock, ACTileSize.DoubleHorizontal, "Cloud", "", "There are 2 new files in your shared cloud", "Cloud");
			group3.AddTile(ACTileStyle.Default, ACTileSize.Quad, "On The Air", "", "", "RadioTower");
			group3.AddTile(ACTileStyle.Default, ACTileSize.Single, "Tags", "", "", "Tag");
			group3.AddTile(ACTileStyle.Default, ACTileSize.Single, "Wallet", "", "", "Wallet");
			group3.AddTile(ACTileStyle.Default, ACTileSize.DoubleVertical, "TV", "", "", "TV");
			group3.AddTile(ACTileStyle.Default, ACTileSize.Single, "Movies", "", "", "Movie");
			group3.AddTile(ACTileStyle.Default, ACTileSize.Single, "Microphone", "", "", "Microphone");
			group3.AddTile(iOSDevice.isPhone ? ACTileStyle.CornerIcon : ACTileStyle.Default, ACTileSize.Quad, iOSDevice.isPhone ? "" : "Current Time", iOSDevice.isPhone ? "12:30" : "12:30pm", "", "Clock");
			group3.AddTile(ACTileStyle.Default, ACTileSize.DoubleHorizontal, "Contrast", "", "", "Contrast");
			group3.AddTile(ACTileStyle.Default, ACTileSize.DoubleHorizontal, "System Check-Up", "", "", "CheckMark");
			group3.AddTile(ACTileStyle.Default, ACTileSize.DoubleHorizontal, "This Week", "", "", "CalendarWeek");
			var pictureStream = group3.AddTile(ACTileStyle.BigPicture, ACTileSize.Quad, iOSDevice.isPhone ? "" : "PictureStream", "42", "", "Boat.jpg");
			group3.AddTile(ACTileStyle.Default, ACTileSize.Single, "Key", "", "", "Key");
			group3.AddTile(ACTileStyle.Default, iOSDevice.isPhone ? ACTileSize.DoubleHorizontal : ACTileSize.DoubleVertical, "Music", "", "", "Music");

			// Add live updates to group
			pictureStream.liveUpdateAction = new ACTileLiveUpdateTileImages(pictureStream, new string[] { "ManWithChild.jpg", "Bridge.jpg", "Arrow.jpg", "Fountain.jpg", "Boat.jpg" });
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
			group4.AddTile(ACTileStyle.Default, ACTileSize.Single, "Monitor", "", "", "Widescreen");
			group4.AddTile(ACTileStyle.Default, ACTileSize.DoubleVertical, "Desktop", "", "", "Arrow.jpg");
			group4.AddTile(ACTileStyle.DescriptionBlock, ACTileSize.DoubleHorizontal, "System Tools", "", "It's been 4 days since your last scan.", "Tools");
			group4.AddTile(ACTileStyle.BigPicture, ACTileSize.Quad, "Feeds", "15", "", "Fountain.jpg");
			group4.AddTile(ACTileStyle.Default, ACTileSize.Single, "locate", "", "", "Scanner");
			group4.AddTile(ACTileStyle.BigPicture, ACTileSize.Single, "", "", "", "Boat.jpg");
			group4.AddTile(ACTileStyle.Default, ACTileSize.Single, "Radio", "", "", "Radio");
			group4.AddTile(ACTileStyle.Default, ACTileSize.Single, "Labels", "", "", "Tag");
			group4.AddTile(ACTileStyle.Default, ACTileSize.Single, "Password", "", "", "Key");

			// Randomily assign a green color to the tiles in this group with the
			// given brightness range
			group4.ChromaKeyTiles(ACColor.ActionSkyBlue, 50, 250);

			// Restore updates
			tileController.suspendUpdates = false;

			// Tell the controller to automatically update itself
			tileController.liveUpdate = true;

			// Display touched tile
			tileController.TileTouched += (group, tile) => {
				Console.WriteLine("Touched tile {0} in group {1}", tile.title, group.title);
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
		#endregion
	}
}
