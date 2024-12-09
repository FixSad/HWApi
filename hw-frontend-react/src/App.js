import './App.css';
import React, { useEffect, useState } from 'react';

function App() {
    const [nodes, setNodes] = useState([]);

    useEffect(() => {
        fetchNodes();
    }, []);

    const fetchNodes = async () => {
        const response = await fetch('http://localhost:5262/api/node');
        const data = await response.json();
        console.log(data);
        setNodes(data);
    };

return (

    <div>
        <h1>Fetched Data</h1>
        <ul>
            {nodes.map((node) => (
                <li key={node.id}>{node.name}</li>
            ))}
        </ul>
    </div>
);
}

export default App;
