using System;
using UIKit;
using ActionComponents;

namespace iOSActionTiles
{
	public partial class SecondViewController : UIViewController
	{
		#region Constructors
		protected SecondViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
		#endregion

		#region Override Methods
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Setup tile controller
			tileController.title = "Home Controller";
			tileController.scrollDirection = ACTileControllerScrollDirection.Vertical;
			tileController.appearance.backgroundImage = UIImage.FromBundle("Agreement.jpg");
			tileController.navigationBar.ShowNavigationBar(true);
			tileController.navigationBar.AddLeftButton("Home", null, 50, 32, (sender, e) => {
				Console.WriteLine("Home button pressed");
			});
			tileController.navigationBar.AddRightButton("Edit", null, 50, 32, (sender, e) => {
				Console.WriteLine("Edit button pressed");
			});
			tileController.navigationBar.AddRightButton("", UIImage.FromBundle("Add"), 16, 16, (sender, e) => {
				Console.WriteLine("Add button pressed");
			});

			// Suspend updates
			tileController.suspendUpdates = true;

			// Adjust based on device
			var sceneSize = 4;
			if (iOSDevice.isPhone)
			{
				tileController.appearance.cellSize = 45f;
				sceneSize = 3;
			}
			else
			{
				tileController.appearance.cellSize = 50f;
			}

			// Style tiles
			tileController.tileAppearance.titleSize = 14f;
			tileController.tileAppearance.subtitleColor = ACColor.FromRGB(100, 100, 100);
			tileController.tileAppearance.subtitleSize = 12f;


			// Add new group
			var scenes = tileController.AddGroup(ACTileGroupType.ExpandingGroup, "Favorite Scenes", "");

			scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Evening", "", "", "Home");
			scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Go to Bed", "", "", "Home");
			scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Night", "", "", "Home");
			scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Start Work", "", "", "Home");
			scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Sunset", "", "", "Home");
			scenes.AddCustomSizedTile(1, sceneSize, ACTileStyle.Scene, "Wake Up", "", "", "Home");

			// Add new group
			var accessories = tileController.AddGroup(ACTileGroupType.ExpandingGroup, "Favorite Accessories", "");

			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Kitchen Eve Door", "No Response", "", "Home");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Patio Eve Humidity", "No Response", "", "Home");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Patio Eve Temp", "No Response", "", "Home");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Bedroom", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Dining Room Left", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Dining Room Right", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Bloom", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Iris", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Lightstrip", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Table Lamp", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Wicker Bottom", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Living Wicker Top", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Office Desk", "Off", "", "Light");
			accessories.AddTile(ACTileStyle.Accessory, ACTileSize.Quad, "Office Side Table", "Off", "", "Light");

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
						tile.icon = UIImage.FromBundle("Brightness");
						break;
					case "On":
						tile.subtitle = "Off";
						tile.appearance.background = ACColor.FromRGBA(213, 213, 213, 240);
						tile.icon = UIImage.FromBundle("Light");
						break;
				}
			};

			// Restore updates
			tileController.suspendUpdates = false;

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
