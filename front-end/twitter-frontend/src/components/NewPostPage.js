import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './NewPostPage.css';
import KeyCloakService from '../security/KeycloakService.tsx';

function NewPostPage() {
  const [message, setMessage] = useState('');
  
  
  const addPost = () => {
    console.log("post added: " + message);
    const post = { message: message, author: KeyCloakService.GetUserName(), date: '2021-03-01', likes: '0' };
    console.log(post);
    axios.post('https://localhost:7237/api/posts', post).then(response => {console.log(response)}).catch(error => {console.error(error)});
    // //axios.post('http://35.205.199.107/api/posts', post).then(response => {console.log(response)}).catch(error => {console.error(error)});
    // //axios.post('http://twitterbutcooler.com/api/posts', post).then(response => {console.log(response)}).catch(error => {console.error(error)});
    // console.log("post added: " + message);
  }
  
  return (
    <div className='FormContainer'>
      <head>
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
      </head>
      <form className='Form'>
        <div className="form-group">
          <label for="exampleInputEmail1"></label>
          <input onChange={(e) => setMessage(e.target.value)}  className="form-control"  placeholder="What's on your mind?" />
          <small id="emailHelp" className="form-text text-muted">Leave a message for your followers.</small>
        </div>
      </form>
        <button  onClick={() => addPost()} type="submit" className="btn btn-primary">Submit</button>
    </div>
  );
}


 export default NewPostPage;