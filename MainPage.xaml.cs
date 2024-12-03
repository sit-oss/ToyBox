namespace ToyBox
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCheckBoxChanged(object sender, CheckedChangedEventArgs e)
        {
            PermissionEntry.Text = CalculatePermissionCode();
        }

        private void OnPermissionEntryChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(e.NewTextValue, out int permissionCode) && permissionCode >= 0 && permissionCode <= 777)
            {
                UpdateCheckBoxes(permissionCode);
            }
        }

        private string CalculatePermissionCode()
        {
            int user = (UserRead.IsChecked ? 4 : 0) + (UserWrite.IsChecked ? 2 : 0) + (UserExec.IsChecked ? 1 : 0);
            int group = (GroupRead.IsChecked ? 4 : 0) + (GroupWrite.IsChecked ? 2 : 0) + (GroupExec.IsChecked ? 1 : 0);
            int other = (OtherRead.IsChecked ? 4 : 0) + (OtherWrite.IsChecked ? 2 : 0) + (OtherExec.IsChecked ? 1 : 0);

            return $"{user}{group}{other}";
        }

        private void UpdateCheckBoxes(int permissionCode)
        {
            int user = permissionCode / 100;
            int group = (permissionCode / 10) % 10;
            int other = permissionCode % 10;

            // Update User Checkboxes
            UserRead.IsChecked = (user & 4) != 0;
            UserWrite.IsChecked = (user & 2) != 0;
            UserExec.IsChecked = (user & 1) != 0;

            // Update Group Checkboxes
            GroupRead.IsChecked = (group & 4) != 0;
            GroupWrite.IsChecked = (group & 2) != 0;
            GroupExec.IsChecked = (group & 1) != 0;

            // Update Other Checkboxes
            OtherRead.IsChecked = (other & 4) != 0;
            OtherWrite.IsChecked = (other & 2) != 0;
            OtherExec.IsChecked = (other & 1) != 0;
        }
    }

}
