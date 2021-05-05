import React from 'react';

import { AiOutlineCalendar } from "react-icons/ai";

import './styles.css';

function Card() {
    return (
        <div className="card-container">
            <header><h3>NOME DO PROJETO</h3></header>
            <p>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit.
            </p>
            <footer>
                <AiOutlineCalendar id="calendar-icon" /> dd/MM/yyyy
            </footer>
        </div>
    );
}

export default Card;