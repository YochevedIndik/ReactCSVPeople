import React from 'react';
import { Route } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './pages/Home';
import Generate from './pages/Generate';
import Upload from './pages/Upload';




const App = () => {
    return (
        <Layout>
            <Route exact path='/' component={Home} />
            <Route exact path='/upload' component={Upload} />
            <Route exact path='/generate' component={Generate} />
        </Layout>
    );
}

export default App;