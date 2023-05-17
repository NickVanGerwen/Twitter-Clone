import logo from './logo.svg';
// import './App.css';
import axios from 'axios';
import { useEffect, useState } from 'react';

function App() {
    const post = { message: 'my non-controversial opinion', author: 'me', date: '2021-03-01', likes: '0' };

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

  const addPost = () => {
    axios.post('http://twitterbutcooler.com/api/posts', post).then(response => {console.log(response)}).catch(error => {console.error(error)});
    console.log("post added");
  }

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
          { (posts.length > 0) ? 
          posts.map(post => (
            <tr key={post.id}>
              <td>{post.id}</td>
              <td>{post.message}</td>
              <td>{post.author}</td>
              <td>{post.date}</td>
              <td>{post.likes}</td>
            </tr>
           ))
          :
          <div>fetching tweets...</div>
          } 
        </tbody>
      </table>
      <button onClick={() => addPost()} type="button" class="btn btn-primary">NEW POST</button>
    </div>

  );
}

export default App;
