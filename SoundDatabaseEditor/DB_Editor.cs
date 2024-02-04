using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasicTwitchSoundPlayer.Extensions;
using BasicTwitchSoundPlayer.Structs;

namespace BasicTwitchSoundPlayer.SoundDatabaseEditor
{
    public partial class DB_Editor : Form
    {
        public const string NodeNameEntry = "Entry";
        public const string NodeNameFiles = "Files";
        public const string NodeNameRequirements = "Requirement";
        public const string NodeDescription = "Description";
        public const string NodeNameDateAdded = "DateAdded";
        private string spreadsheetId = "";

        public List<SoundEntry> Sounds;
        char PrefixCharacter;

        public DB_Editor(List<SoundEntry> Sounds, char PrefixCharacter)
        {
            this.PrefixCharacter = PrefixCharacter;
            this.Sounds = Sounds;
            this.spreadsheetId = PrivateSettings.GetInstance().GoogleSpreadsheetID;
            InitializeComponent();
        }

        private void DB_Editor_Load(object sender, EventArgs e)
        {
            foreach(var Sound in Sounds)
            {
                sndTreeView.Nodes.Add(Sound.ToTreeNode());
            }
        }

        private TreeNode GetRootSoundNode(TreeNode Node)
        {
            while(Node.Parent != null)
            {
                Node = Node.Parent;
            }
            return Node;
        }

        private void EditEntry()
        {
            if (sndTreeView.SelectedNode != null)
            {
                var SndNode = GetRootSoundNode(sndTreeView.SelectedNode);
                int index = sndTreeView.SelectedNode.Index;
                var dialForm = new EditDialogues.AddEditNewEntryDialog(SndNode.ToSoundEntry());
                DialogResult res = dialForm.ShowDialog();

                if(res == DialogResult.OK)
                {
                    sndTreeView.Nodes[index].Remove();
                    sndTreeView.Nodes.Insert(index, dialForm.ReturnSound.ToTreeNode());
                }
            }
        }

        private void RemoveEntry()
        {
            if(sndTreeView.SelectedNode != null)
            {
                var SndNode = GetRootSoundNode(sndTreeView.SelectedNode);
                sndTreeView.Nodes.Remove(SndNode);
            }
        }

        #region ButtonEvents
        private void SndTreeView_DoubleClick(object sender, EventArgs e)
        {
            var SelectedNode = sndTreeView.SelectedNode;
            if (SelectedNode != null)
            {
                var RootSndNode = GetRootSoundNode(SelectedNode);
                sndTreeView.SelectedNode = RootSndNode;
                EditEntry();
            }
        }

        private void B_Save_Click(object sender, EventArgs e)
        {
            this.Sounds = TreeNodesToSoundsEntryList(sndTreeView);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private List<SoundEntry> TreeNodesToSoundsEntryList(TreeView tree)
        {
            this.Sounds = new List<SoundEntry>();  //Cause I can't be bothered to test whatever it's a hidden pointer and propages everywhere or not
            for(int i=0; i<sndTreeView.Nodes.Count; i++)
            {
                var newSnd = sndTreeView.Nodes[i].ToSoundEntry();
                if (newSnd.GetIsProperEntry())
                    Sounds.Add(newSnd);
            }
            return Sounds;

        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void B_AddEntry_Click(object sender, EventArgs e)
        {
            EditDialogues.AddEditNewEntryDialog newEntryDialog = new EditDialogues.AddEditNewEntryDialog();
            DialogResult res = newEntryDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                sndTreeView.Nodes.Add(newEntryDialog.ReturnSound.ToTreeNode());
            }
        }


        private void B_Edit_Click(object sender, EventArgs e)
        {
            EditEntry();
        }

        private void B_RemoveEntry_Click(object sender, EventArgs e)
        {
            RemoveEntry();
        }
        #endregion

        #region PressKeyEvents
        private void SndTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if(sndTreeView.SelectedNode != null)
                {
                    var parent = GetRootSoundNode(sndTreeView.SelectedNode);

                    sndTreeView.Nodes.Remove(parent);
                }
            }
            else if(e.KeyCode == Keys.Enter)
            {
                if(sndTreeView.SelectedNode != null)
                {
                    EditEntry();
                }
            }
        }
        #endregion

        private string GetHTMLColor(TwitchRightsEnum twitchRightsEnum)
        {
            switch(twitchRightsEnum)
            {
                case TwitchRightsEnum.Admin:
                    return "#FF0000";
                case TwitchRightsEnum.Mod:
                    return "#00FF00";
                case TwitchRightsEnum.TrustedSub:
                    return "#F0F0FF";
                case TwitchRightsEnum.Public:
                    return "#FFFF90";
                default:
                    return "#0000FF";
            }
        }

        private void B_Sort_Click(object sender, EventArgs e)
        {
            sndTreeView.Sort();
        }

        private void B_Export_Click(object sender, EventArgs e)
        {
            using (EditDialogues.ExportDialog exDialog = new EditDialogues.ExportDialog())
            {
                var result = exDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (exDialog.ExportType == EditDialogues.ExportType.HTML)
                    {
                        this.ExportToHTML();
                    }
                    else
                    {
                        this.ExportToGoogleDoc();
                    }
                }
            }
        }

        private void B_SoundPlayBackSettings_Click(object sender, EventArgs e)
        {
            using (EditDialogues.SoundPlaybackSettingsDialog spsDialog = new EditDialogues.SoundPlaybackSettingsDialog())
            {
                var result = spsDialog.ShowDialog();
                if(result == DialogResult.OK)
                {
                    var setings = PrivateSettings.GetInstance();
					setings.SoundRewardID = spsDialog.SoundRewardID;
					setings.SoundRedemptionLogic = spsDialog.SoundLogic;
					setings.OutputDevice = spsDialog.SelectedDevice;
					setings.SaveSettings();
                }
            }
        }

        #region ExportFunctions
        private void ExportToHTML()
        {
            var dialog = new SaveFileDialog()
            {
                Filter = "HTML file (*.html)|*.html"
            };
            DialogResult res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                var ExportSounds = TreeNodesToSoundsEntryList(sndTreeView);
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("<!DOCTYPE HTML PUBLIC \" -//W3C//DTD HTML 4.0 Transitional//EN\">");
                sb.AppendLine("");
                sb.AppendLine("<html>");
                sb.AppendLine("<head>");
                sb.AppendLine("	<meta http-equiv=\"content - type\" content=\"text / html; charset = windows - 1250\"/>");
                sb.AppendLine("<title></title>");
                sb.AppendLine("<style type=\"text / css\">");
                sb.AppendLine("\t\tbody, div, table, thead, tbody, tfoot, tr, th, td, p { font - family:\"Arial\"; font - size:x - small }");
                sb.AppendLine("\t\ta.comment-indicator:hover + comment { background:#ffd; position:absolute; display:block; border:1px solid black; padding:0.5em;  } ");
                sb.AppendLine("\t\ta.comment - indicator { background: red; display: inline - block; border: 1px solid black; width: 0.5em; height: 0.5em; }");
                sb.AppendLine("comment { display:none;  } ");
                sb.AppendLine("</style>");
                sb.AppendLine("</head>");
                sb.AppendLine("<body>");
                sb.AppendLine("<table cellspacing=\"0\" border=\"1\">");
                sb.AppendLine("<colgroup span=\"3\" width=\"1000\"></colgroup>");
                sb.AppendLine("<tr>" +
                    "<td height=\"21\" align=\"left\"><b>(" + PrefixCharacter + ") Command (total: " + ExportSounds.Count.ToString() + ")</b></td>" +
                    "<td align=\"left\"><b>File</b></td>" +
                    "<td align=\"left\"><b>Description</b></td>" +
                    "<td align=\"left\"><b>Added (UTC)</b></td>" +
                    "</tr>");
                foreach (var snd in ExportSounds)
                {
                    sb.AppendLine(GetTableRowForFile(snd));
                }
                sb.AppendLine("</table>");
                sb.AppendLine("</body>");
                sb.AppendLine("</html>");
                System.IO.File.WriteAllText(dialog.FileName, sb.ToString());
                System.Diagnostics.Process.Start(dialog.FileName);
            }
        }

        private string GetTableRowForFile(SoundEntry hSound)
        {
            return string.Format("<tr>" +
                "<td height=\"21\" align=\"left\" bgcolor=\"{5}\">{0}{1}</td>" +
                "<td height=\"21\" align=\"left\" bgcolor=\"{5}\">{2}</td>" +
                "<td height=\"21\" align=\"left\" bgcolor=\"{5}\">{3}</td>" +
                "<td height=\"21\" align=\"left\" bgcolor=\"{5}\">{4}</td>" +
                "</tr>",
                PrefixCharacter,
                hSound.GetCommand(),
                hSound.GetAllFiles().Length > 1 ? "multiple" : System.IO.Path.GetFileNameWithoutExtension(hSound.GetAllFiles().First()),
                hSound.GetDescription(),
                hSound.GetDateAdded().ToString(),
                GetHTMLColor(hSound.GetRequirement())
                );

        }

        private void ExportToGoogleDoc()
        {
            var tmpSnd = TreeNodesToSoundsEntryList(sndTreeView);

            var settings = PrivateSettings.GetInstance();
            GoogleSheetsExport sheet = new GoogleSheetsExport(spreadsheetId, settings.SoundRedemptionLogic, PrefixCharacter, tmpSnd);
            if (sheet.WasSuccess)
                MessageBox.Show("New sound db successfully exported to Google Spreadsheet", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        #endregion
    }

    #region Extensions
    static class EditorExtensions
    {
        public static SoundEntry ToSoundEntry(this TreeNode node)
        {
            if (node.Name == DB_Editor.NodeNameEntry)
            {
                string Command = node.Text;
                TwitchRightsEnum Right = node.Nodes[DB_Editor.NodeNameRequirements].Text.ToTwitchRights();
                string[] Files = new string[node.Nodes[DB_Editor.NodeNameFiles].Nodes.Count];
                string Description = node.Nodes[DB_Editor.NodeDescription].Text;
                DateTime DateAdded = node.Nodes[DB_Editor.NodeNameDateAdded].Text.ToDateTimeSafe();

                for (int i = 0; i < Files.Length; i++)
                {
                    Files[i] = node.Nodes[DB_Editor.NodeNameFiles].Nodes[i].Text;
                }

                return new SoundEntry(Command, Right, Files, Description, DateAdded);
            }
            else
                return new SoundEntry();
        }

        public static TreeNode ToTreeNode(this SoundEntry snd)
        {
            if (snd.GetIsProperEntry())
            {
                var newNode = new TreeNode(snd.GetCommand())
                {
                    Name = DB_Editor.NodeNameEntry,
                    ImageIndex = 0
                };

                var FilesNode = newNode.Nodes.Add(DB_Editor.NodeNameFiles);
                FilesNode.ImageIndex = 1;
                FilesNode.SelectedImageIndex = 1;
                FilesNode.StateImageIndex = 1;
                FilesNode.Name = DB_Editor.NodeNameFiles;
                foreach (var file in snd.GetAllFiles())
                {
                    if(file.RemoveWhitespaces() != String.Empty)
                    {
                        var fNode = FilesNode.Nodes.Add(file);
                        fNode.ImageIndex = 1;
                        fNode.SelectedImageIndex = 1;
                        fNode.StateImageIndex = 1;
                    }
                }

                var Requirement = newNode.Nodes.Add(DB_Editor.NodeNameRequirements);
                Requirement.ImageIndex = 2;
                Requirement.SelectedImageIndex = 2;
                Requirement.StateImageIndex = 2;
                Requirement.Name = DB_Editor.NodeNameRequirements;
                Requirement.Text = snd.GetRequirement().ToString();

                var Description = newNode.Nodes.Add(DB_Editor.NodeDescription);
                Description.ImageIndex = 3;
                Description.SelectedImageIndex = 3;
                Description.StateImageIndex = 3;
                Description.Name = DB_Editor.NodeDescription;
                Description.Text = snd.GetDescription();

                var DateTimeTreeNode = newNode.Nodes.Add(DB_Editor.NodeNameDateAdded);
                DateTimeTreeNode.ImageIndex = 4;
                DateTimeTreeNode.SelectedImageIndex = 4;
                DateTimeTreeNode.StateImageIndex = 4;
                DateTimeTreeNode.Name = DB_Editor.NodeNameDateAdded;
                DateTimeTreeNode.Text = snd.GetDateAdded().ToString();
                return newNode;
            }
            else
                throw new Exception(snd.GetCommand() + " is an incorrect entry!");
        }
    }
    #endregion
}
