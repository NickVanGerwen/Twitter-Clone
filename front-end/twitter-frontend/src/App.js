import LoginPage from './components/LoginPage';
import HomePage from './components/HomePage';
import NewPostPage from './components/NewPostPage';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import KeyCloakService from './security/KeycloakService.tsx';

function App() {
  return (
    <>
      <header>
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
      </header>

      <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <a class="navbar-brand" href="http://localhost:3000/Home">Twitter but cooler</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="navbar-nav mr-auto">
            <li class="nav-item active">
              <a class="nav-link" href="http://localhost:3000/Home">Home <span class="sr-only">(current)</span></a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="http://localhost:3000/NewPost">Send a tweet!</a>
            </li>
          </ul>
          <form class="form-inline my-2 my-lg-0">
            <div style={{paddingRight: 12} } >{KeyCloakService.GetUserName()}</div>
            {/* <button onClick={KeyCloakService.CallLogout}>log out</button> */}
            <button onClick={KeyCloakService.CallLogout}  type="button" class="btn btn-outline-danger">Log out</button>
          </form>
        </div>
      </nav>

      <Router>
        <Switch>
          <Route path='/Home' exact component={HomePage} />
          <Route path='/' exact component={HomePage} />
          <Route path='/NewPost' exact component={NewPostPage} />
        </Switch>
      </Router>
    </>
  );
}

export default App;