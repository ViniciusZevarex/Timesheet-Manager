import React from 'react';

import Menu from '../../Components/Menu';
import Card from '../../Components/Card';

import './styles.css';

function Home() {
    return (
        <div className="home-container">
            <Menu />

            <Card />
        </div>        
    );
}

export default Home;