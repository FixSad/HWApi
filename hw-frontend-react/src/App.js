import './App.css';
import TreeView from './TreeView';
import React, { useEffect, useState } from 'react';

function App() {
    const [nodes, setNodes] = useState([]);
    const [parentId, setParentId] = useState('');
    const [name, setName] = useState('');

    useEffect(() => {
        fetchNodes();
    }, []);

    const fetchNodes = async () => {
        const response = await fetch('http://localhost:5262/api/node');
        const data = await response.json();
        setNodes(data);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            await fetch('http://localhost:5262/api/node', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ parentId, name }),
            }).then((response) => {
                if(response.ok)
                    alert("Node was successfully created");                
                else
                    alert("Incorrect data");

                setName('');
                setParentId('');
            });
        }
        catch(error){
            console.log(error);
        }
    };



    return (
        <>
            <div>
                <h1>Tree</h1>
                <TreeView data={nodes} />
            </div>
            <div>
                <h1>Create Node</h1>
                <form>
                    <div>If you want to create main node, leave field Parent Id empty</div>
                    <div>
                        <label>Parent Id:</label>
                        <input
                            type="text"
                            value={parentId}
                            onChange={(e) => setParentId(e.target.value)}
                            required
                        />
                    </div>

                    <div>
                        <label>Body:</label>
                        <textarea
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            required
                        />
                    </div>

                    <button type="submit" onClick={handleSubmit}>Submit</button>
                </form>
            </div>
        </>
    );
}

export default App;
