using Microsoft.AspNetCore.Mvc;
using HWBackend.Models;
using HWBackend.DataAccess;
using HWBackend.Contracts;

namespace MyApp.Namespace
{
    [Route("api/node")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public NodeController(ApplicationDBContext dbContext) => _dbContext = dbContext;

        [HttpPost]
        public async Task<IActionResult> Create(CreateNode node)
        {
            try
            {
                if(node.parentId != "")
                    Int32.Parse(node.parentId);

                Node tmpNode = new Node{
                    Name = node.name,
                    ParentId = node.parentId
                };
                
                await _dbContext.Nodes.AddAsync(tmpNode);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var nodes = _dbContext.Nodes;
                var map = new Dictionary<int, TreeNode>();
                var rootNodes = new List<TreeNode>();

                foreach(var node in nodes)
                {
                    var newNode = new TreeNode
                    {
                        Id = node.Id,
                        Name = node.Name,
                        Children = new List<TreeNode>()
                    };

                    if (node.ParentId == "")
                        rootNodes.Add(newNode);
                    
                    map[newNode.Id] = newNode;
                }

                foreach(var node in nodes)
                {
                    if (node.ParentId != "")
                    {
                        var parentNode = map[int.Parse(node.ParentId)];
                        parentNode.Children.Add(map[node.Id]);
                    }
                }

                return Ok(rootNodes);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var node = _dbContext.Nodes.
                    Where(n => n.Id == id).FirstOrDefault();
                
                if(node != null)
                    return Ok(node);

                return Ok("No node with that id");
            }
            catch(Exception ex)
            {
                System.Console.WriteLine("Id " + id);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var node = _dbContext.Nodes.
                    Where(n => n.Id.Equals(id)).FirstOrDefault();
                    
                if(node == null)
                    return Ok("No node with that id");
                
                _dbContext.Nodes.Remove(node);
                await _dbContext.SaveChangesAsync();
                return Ok($"Node N:{id} was successfully deleted");
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id}/{name}")]
        public async Task<IActionResult> ChangeNodeName(string name, int id)
        {
            try
            {
                var node = _dbContext.Nodes.
                    Where(n => n.Id == id).FirstOrDefault();

                if(node == null)
                    return Ok("No node with that id");

                node.Name = name;
                _dbContext.Nodes.Update(node);
                await _dbContext.SaveChangesAsync();
                return Ok($"Node N:{id} was successfully updated");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
