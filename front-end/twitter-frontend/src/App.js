import LoginPage from './components/LoginPage';
import HomePage from './components/HomePage';
import NewPostPage from './components/NewPostPage';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';

function App() {
  return (
    <Router>
      <Switch>
        <Route path='/Login' exact component={LoginPage} />
        <Route path='/Home' exact component={HomePage} />
        <Route path='/NewPost' exact component={NewPostPage} />
      </Switch>
    </Router>
  );
}

export default App;