using CompucareWard.Controls.AttachedProperties;
using CompucareWard.iOS.Helpers;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ViewCell), typeof(CompucareWard.iOS.Renderers.StandardViewCellRenderer))]
namespace CompucareWard.iOS.Renderers
{
    public class StandardViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            switch (item.StyleId)
            {
                case "checkmark":
                    if (cell.Accessory != UITableViewCellAccessory.Checkmark)
                        cell.Accessory = UITableViewCellAccessory.Checkmark;
                    break;
                case "detail-button":
                    if (cell.Accessory != UITableViewCellAccessory.DetailButton)
                        cell.Accessory = UITableViewCellAccessory.DetailButton;
                    break;
                case "detail-disclosure-button":
                    if (cell.Accessory != UITableViewCellAccessory.DetailDisclosureButton)
                        cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
                    break;
                case "disclosure":
                    if (cell.Accessory != UITableViewCellAccessory.DisclosureIndicator)
                        cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
                    break;
                case "none":
                default:
                    if (cell.Accessory != UITableViewCellAccessory.None)
                        cell.Accessory = UITableViewCellAccessory.None;
                    break;
            }

            return cell;
        }
    }
}
