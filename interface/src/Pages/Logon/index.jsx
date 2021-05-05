import React from 'react';

import LoginImage from '../../Assets/img/login-form-img.png';

import api from '../../Services/api';

import './index.css';

export default class Logon extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            Email: "",
            Password: ""
        };
    }

    handleEmail = (event) => {
        this.setState({Email: event.target.value});
    }
    handlePassword = (event) => {
        this.setState({Password: event.target.value});
    }

    handleAuth = (event) => {
        event.preventDefault();

        console.log(this.state);

        api.post('v1/auth/login', this.state)
            .then(res => {
                console.log(res);
                console.log(res.data);
        });
    }

    render () {
        return (
            <div className="login-container">
                <form className="login-form" onSubmit={this.handleAuth}>
                    <h1 className="text-center">Entrar</h1>
                    
                    <img src={LoginImage} alt="login image" className="logoForm" />
                    
                    <div className="input-group">
                        <label for="user">Usuário:</label>
                        <input 
                            id="user" 
                            name="user" 
                            type="text" 
                            onChange={this.handleEmail}
                        />
                    </div>
                    
                    <div className="input-group">
                        <label for="password">Senha:</label>
                        <input 
                            id="password" 
                            name="password" 
                            type="text" 
                            onChange={this.handlePassword}
                        />
                    </div>
    
                    <button className="btn-primary" type="submit">
                        Entrar
                    </button>
                </form>
            </div>
        );
    }         
}