import logo from './logo.svg';
import './App.css';
import axios from 'axios';
import { useEffect, useState } from 'react';

function App() {

  const [posts, setPosts] = useState([]);

  useEffect(() => {
    axios.get('http://twitterbutcooler.com/api/fetch')
      .then(response => {
        setPosts(response.data);
      })
      .catch(error => {
        console.error(error);
      });
  }, []);


  return (
    <div className="App">
         <ul>
      {posts.map(post => (
        <li key={post.id}>{post.title}</li>
      ))}
    </ul>
      <head>
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
      </head>
      <table className='table'>
        <thead>
          <tr>
            <th>id</th>
            <th>Message</th>
            <th>Author</th>
            <th>Time</th>
            <th>Likes</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>posts</td>
            <td>My first message</td>
            <td>John Doe</td>
            <td>12:00</td>
            <td>0</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
}

export default App;
