import React from 'react';
import { Link } from 'react-router-dom';

import './index.css';

import LoginImage from '../../Assets/img/login-form-img.png';

export default function Logon()
{
    return (
        <div className="login-container">
            <form className="login-form">
                <h1 className="text-center">Entrar</h1>
                
                <img src={LoginImage} alt="login image" className="logoForm" />
                
                <div className="input-group">
                    <label for="user">Usuário:</label>
                    <input id="user" name="user" type="text" />
                </div>
                
                <div className="input-group">
                    <label for="password">Senha:</label>
                    <input id="password" name="password" type="text" />
                </div>

                <button className="btn-primary">
                    Entrar
                </button>

                <Link to="/home" className="btn-primary">
                    Link to Home, only for dev!
                </Link>
            </form>
        </div>
    )        
}