import React from 'react';
import { Link } from 'react-router-dom';

import { AiOutlineAppstore, AiOutlineHistory, AiFillFunnelPlot } from "react-icons/ai";

import './styles.css';

function Menu() {
    return (
        <div className="menu-container">
            <header>TS. Manager</header>
            <ul>
                <li>
                    <a href="#">
                        <img 
                            className="menu-user" 
                            src="https://avatars.githubusercontent.com/u/41579743?v=4" 
                            alt="user"
                        />
                    </a>
                </li>
                <li><Link to="/home"><AiOutlineAppstore className="menu-icons" />Projects</Link></li>
                <li><a href="#"><AiOutlineHistory className="menu-icons" />Time</a></li>
                <li><a href="#"><AiFillFunnelPlot className="menu-icons" />Search</a></li>
            </ul>
            <footer>SAP B1</footer>
        </div>
    );
}

export default Menu;