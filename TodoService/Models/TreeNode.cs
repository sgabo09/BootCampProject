using System.Collections.Generic;

namespace TodoService.Models
{
    public class TreeNode
    {
        public Todo Node { get; set; }
        public List<TreeNode> Children { get; set; }

        public TreeNode()
        {
            Children = new List<TreeNode>();
        }
    }
}