import { useEffect, useState } from 'react';
import axios from 'axios';



function HomePage(){

  const [posts, setPosts] = useState([]);

  useEffect(() => {
    axios.get('http://35.205.199.107/api/fetch')
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
          {posts.map(post => (
            <tr key={post.id}>
              <td>{post.id}</td>
              <td>{post.message}</td>
              <td>{post.author}</td>
              <td>{post.date}</td>
              <td>{post.likes}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>

  );
}

export default HomePage;
