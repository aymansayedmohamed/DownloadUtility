import React from 'react';
import {Link} from 'react-router';

class HomePage extends React.Component{
    render(){
        return(
            <div className="jumbotron">
                <h1>Download Utility</h1>
                <p>Download Utility enable you to download data from multiple sources and protocols to local disk</p>
                <Link to="about" className="btn btn-primary btn-lg">Learn more</Link>
            </div>
        );
    }
}

export default HomePage;