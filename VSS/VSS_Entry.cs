using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.VSS
{
    public class VSS_Entry
    {
        private enum Icons
        {
            Root,
            Category,
            Sound
        }

        public Keys Hotkey { get; protected set; }
        public string Name { get; protected set; }
        public VSS_Entry Parent { get; set; }
        public bool HasParent => Parent != null;

        protected VSS_Entry()
        {
            this.Parent = null;
            this.Hotkey = Keys.None;
        }

        public VSS_Entry(string Name, Keys Hotkey)
        {
            this.Parent = null;
            this.Hotkey = Hotkey;
            this.Name = Name;
        }

        public VSS_Entry(string Name, Keys Hotkey, VSS_Entry Parent)
        {
            this.Parent = Parent;
            this.Hotkey = Hotkey;
            this.Name = Name;
        }

        public static TreeNode ConvertVSSEntryToTreeNode(VSS_Entry node)
        {
            if (node.GetType() == typeof(VSS_Entry_Group))
            {
                var casted = (VSS_Entry_Group)node;

                var newTreeNode = new TreeNode(casted.Hotkey.ToString() + " - " + casted.Name)
                {
                    Name = node.GetType().ToString(),
                    ImageIndex = (int)Icons.Category,
                    SelectedImageIndex = (int)Icons.Category,
                    StateImageIndex = (int)Icons.Category
                };

                for (int i = 0; i < casted.ChildrenCount; i++)
                {
                    newTreeNode.Nodes.Add(ConvertVSSEntryToTreeNode(casted.GetChildIndex(i)));
                }

                return newTreeNode;
            }
            else if (node.GetType() == typeof(VSS_Entry_Sound))
            {
                var casted = (VSS_Entry_Sound)node;

                var newTreeNode = new TreeNode(casted.Hotkey.ToString() + " - " + casted.Name)
                {
                    Name = node.GetType().ToString(),
                    ImageIndex = (int)Icons.Sound,
                    SelectedImageIndex = (int)Icons.Sound,
                    StateImageIndex = (int)Icons.Sound
                };
                return newTreeNode;
            }
            else
                throw new Exception("Incorrect type");
        }
    }

    //Inhereted Variants
    public class VSS_Entry_Sound : VSS_Entry
    {
        public string Filepath { get; set; }

        public VSS_Entry_Sound(string Name, Keys Hotkey, string Filepath)
        {
            this.Name = Name;
            this.Hotkey = Hotkey;
            this.Filepath = Filepath;
        }

        public VSS_Entry_Sound(string Name, Keys Hotkey, VSS_Entry Parent, string Filepath)
        {
            this.Name = Name;
            this.Hotkey = Hotkey;
            this.Parent = Parent;
            this.Filepath = Filepath;
        }
    }

    public class VSS_Entry_Group : VSS_Entry
    {
        private List<VSS_Entry> Children { get; set; }
        public int ChildrenCount { get { return Children.Count; } }

        public VSS_Entry_Group(string Name, Keys Hotkey)
        {
            this.Name = Name;
            this.Hotkey = Hotkey;
            this.Parent = null;
            Children = new List<VSS_Entry>();
        }

        public VSS_Entry_Group(string Name, Keys Hotkey, VSS_Entry Parent)
        {
            this.Name = Name;
            this.Hotkey = Hotkey;
            this.Parent = Parent;
            Children = new List<VSS_Entry>();
        }

        public VSS_Entry AddChild(VSS_Entry Entry)
        {
            Entry.Parent = this;
            Children.Add(Entry);
            return Entry;
        }

        public void AddChildRange(IEnumerable<VSS_Entry> Collection)
        {
            foreach (var element in Collection)
            {
                element.Parent = this;
            }
            Children.AddRange(Collection);

        }

        public VSS_Entry GetChildFirst()
        {
            if (Children.Count > 0)
                return Children[0];
            else
                return null;
        }

        public VSS_Entry GetChildLast()
        {
            if (Children.Count > 0)
                return Children[Children.Count - 1];
            else
                return null;
        }

        public void ChildrenRemove(int i)
        {
            if (Children.Count > 0 && Children.Count < Children.Count)
                Children.RemoveAt(i);

            throw new IndexOutOfRangeException(i + " is out of array bounds!");
        }

        public void ChildrenRemove(string Name)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i].Name == Name)
                {
                    Children.RemoveAt(i);
                    return;
                }
            }

            throw new KeyNotFoundException("Child \"" + Name + "\" was not found");
        }

        public void ChildrenRemoveFirst()
        {
            Children.RemoveAt(0);
        }

        public void ChildrenRemoveLast()
        {
            if (ChildrenCount > 0)
            {
                Children.RemoveAt(ChildrenCount - 1);
            }
        }

        public VSS_Entry GetChildIndex(int i)
        {
            if (i < Children.Count && Children.Count > 0)
            {
                return Children[i];
            }
            else
            {
                return null;
            }
        }
    }
}