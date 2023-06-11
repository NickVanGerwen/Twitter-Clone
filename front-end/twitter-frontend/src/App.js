import LoginPage from './components/LoginPage';
import HomePage from './components/HomePage';
import NewPostPage from './components/NewPostPage';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';



function App() {

return(
  <Router>
  <Switch>
  <Route path='/Login' exact component={LoginPage} />
  <Route path='/Home' exact component={HomePage} />
  <Route path='/NewPost' exact component={NewPostPage} />

  </Switch>
  </Router>
);}

export default App;

 
//     const post = { message: 'cool opinion online', author: 'cool guy', date: '2021-03-01', likes: '0' };

//   const [posts, setPosts] = useState([]);

//   useEffect(() => {
//     axios.get('http://35.205.199.107/api/fetch')
//       .then(response => {
//         setPosts(response.data);
//       })
//       .catch(error => {
//         console.error(error);
//       });
//   }, []);

//   const addPost = () => {
//     axios.post('http://35.205.199.107/api/posts', post).then(response => {console.log(response)}).catch(error => {console.error(error)});
//     console.log("post added");
//   }

//   return (
//     <div className="App">
//       <ul>

//       </ul>
//       <head>
//         <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
//       </head>
//       <table className='table'>
//         <thead>
//           <tr>
//             <th>id</th>
//             <th>Message</th>
//             <th>Author</th>
//             <th>Time</th>
//             <th>Likes</th>
//           </tr>
//         </thead>
//         <tbody>
//           {posts.map(post => (
//             <tr key={post.id}>
//               <td>{post.id}</td>
//               <td>{post.message}</td>
//               <td>{post.author}</td>
//               <td>{post.date}</td>
//               <td>{post.likes}</td>
//             </tr>
//           ))}
//         </tbody>
//       </table>
//       <button onClick={() => addPost()} type="button" class="btn btn-primary">NEW POST</button>
//     </div>

//   );
// }

