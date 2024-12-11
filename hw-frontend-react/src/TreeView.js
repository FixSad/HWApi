import React from 'react';
import { useEffect, useState } from 'react';

const TreeNode = ({ node }) => {
    const [aNode, setANode] = useState(null);
    const [deleteResponse, setDeleteResponse] = useState([]);

    useEffect(() => {
        setANode(node);
    }, []);

    const handleDelete = async () => {
        try {
            var params = new URLSearchParams({
                id: aNode.id
            });

            await fetch('http://localhost:5262/api/node?' + params.toString(), {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                },
            }).then((response) => {
                if(response.ok)
                    window.location.reload();          
                else
                    alert("Error");
            });
        }
        catch(error){
            console.log(error);
        }
    }
    
    return (
        <div style={{ marginLeft: '20px' }}>
            <div>
                <strong>{node.name}</strong> (ID: {node.id})
                <button onClick={handleDelete}>Delete</button>
            </div>
            {node.children && node.children.length > 0 && (
                <div>
                    {node.children.map((child) => (
                        <TreeNode key={child.id} node={child} />
                    ))}
                </div>
            )}
        </div>
    );
};

const TreeView = ({ data }) => {
    return (
        <div>
            {data.map((node) => (
                <TreeNode key={node.id} node={node} />
            ))}
        </div>
    );
};

export default TreeView;