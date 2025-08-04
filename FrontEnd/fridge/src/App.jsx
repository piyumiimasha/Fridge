import React, { useState, useEffect } from "react";
import axios from "axios";
import './App.css';

function App() {
  const [items, setItems] = useState([]);
  const [newItem, setNewItem] = useState({ name: "", expiryDate: "" });

  // Fetch all food items from the backend
  useEffect(() => {
    axios.get("http://localhost:5155/api/food") // Adjust the port to your backend port
      .then(response => {
        setItems(response.data);
      })
      .catch(error => {
        console.error("Error fetching food items:", error);
      });
  }, []);

  // Handle adding a new item
  const handleAddItem = () => {
    
    if (newItem.name && newItem.expiryDate) {
      const isoDate = new Date(newItem.expiryDate).toISOString();
      axios.post("http://localhost:5155/api/food", {
        name: newItem.name,
        expiryDate: newItem.expiryDate
      })
      .then(response => {
        console.log("Response from server:", response.data);
        setItems([...items, response.data]);
        setNewItem({ name: "", expiryDate: "" });
      })
      .catch(error => {
        console.error("Error adding item:", error);
      });
    } else {
      alert("Please fill in both fields");
    }
  };

  // Handle deleting an item
  const handleDeleteItem = (id) => {
    axios.delete(`http://localhost:5155/api/food/${id}`)
      .then(() => {
        setItems(items.filter(item => item.id !== id));
      })
      .catch(error => {
        console.error("Error deleting item:", error);
      });
  };

  return (
    <div className="flex flex-col items-center p-6 bg-gray-50 min-h-screen">
      <h1 className="text-3xl font-bold text-center mt-10">Good Morning, Johny!</h1>
      <p className="text-center text-gray-600 mt-2">🌟 It's better to go shopping before this Friday</p>

      <div className="bg-white shadow-md rounded-lg p-6 mt-6 w-full max-w-3xl">
        <div className="flex flex-col md:flex-row items-center justify-between space-y-4 md:space-y-0">
          <div className="flex items-center space-x-2 w-full md:w-auto">
            <span role="img" aria-label="item" className="text-red-500">🌶️</span>
            <input
              type="text"
              placeholder="Item Name"
              value={newItem.name}
              onChange={(e) => setNewItem({ ...newItem, name: e.target.value })}
              className="border border-gray-300 rounded-md p-2 w-full md:w-64"
            />
          </div>
          <div className="flex items-center space-x-2 w-full md:w-auto">
            <span role="img" aria-label="date" className="text-red-500">⏰</span>
            <input
              type="date"
              placeholder="Expiry Date"
              value={newItem.expiryDate}
              onChange={(e) => setNewItem({ ...newItem, expiryDate: e.target.value })}
              className="border border-gray-300 rounded-md p-2 w-full md:w-64"
            />
          </div>
          <button
            className="bg-blue-600 text-white rounded-md px-4 py-2"
            onClick={handleAddItem} 
            //disabled={!newItem.name || !newItem.expiryDate}
          >
            ADD TO FRIDGE
          </button>
        </div>
      </div>

      <div className="bg-blue-50 shadow-md rounded-lg p-6 mt-6 w-full max-w-3xl">
        <div className="flex justify-between items-center mb-4">
          <span className="text-gray-600">Total items — {items.length}</span>
        </div>
        <div className="space-y-4">
          {items.map((item, index) => (
            <div key={index} className="flex justify-between items-center bg-white p-4 rounded-md shadow-sm">
              <div>
                <span className="font-semibold">{item.name}</span>
                <span className="text-gray-500 ml-2">Expiry date — {item.expiryDate}</span>
              </div>
              <div className="flex items-center space-x-2">
                <span className={`px-2 py-1 rounded bg-green-100 text-green-600`}>{item.status}</span>
                <i
                  className="fas fa-trash-alt text-gray-400 cursor-pointer"
                  onClick={() => handleDeleteItem(item.id)}
                ></i>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default App;
