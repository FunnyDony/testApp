//import {Queries} from './queries';
import {isValid, isValidCity} from './util';
import './styles.css'

const formip = document.getElementById('formIP')
const formcity = document.getElementById('formCity')

const ip_input = formip.querySelector('#ip-input')
const city_input = formcity.querySelector('#city-input')

const ip_submit = formip.querySelector('#ip-submit')
const city_submit = formip.querySelector('#city-submit')



formip.addEventListener('submit',submitIpFormHandler)
ip_input.addEventListener('input', ()=>{
    ip_submit.disabled = !isValid(ip_input.value)
})

async function submitIpFormHandler(event) {
    event.preventDefault()
    if (isValid(ip_input.value)) {
        const query = {
            test: ip_input.value,
            data: new Date().toJSON()
        }

        ip_submit.disabled = true
        // отправка запроса на сервак


        let url = 'https://localhost:44353/ip/location?ip='+ ip_input.value
        let response = await fetch(url)
        let commits = await response.json()

        console.log('1', commits)
        ip_input.value = ''
        ip_input.className = ''
        ip_submit.disabled = false



        const list = document.getElementById('ipContainer')
        list.innerText = JSON.stringify(commits)
    }
}

function addLocStorage(commits)
{
    localStorage.setItem('commits',JSON.stringify(commits))
}

formcity.addEventListener('submit',submitCityFormHandler)
city_input.addEventListener('input', ()=>{
   // city_submit.disabled = isValidCity(city_input.value)
})

async function submitCityFormHandler(event) {
    event.preventDefault()
    if (isValidCity(city_input.value)) {
        const query = {
            test: city_input.value,
            data: new Date().toJSON()
        }

        //city_submit.disabled = true

        let url = 'https://localhost:44353/city/locations?city=' + city_input.value
        let response = await fetch(url)
        let commits = await response.json()

        console.log('1', commits)
        city_input.value = ''
        city_input.className = ''
        city_input.disabled = false



        const list = document.getElementById('cityContainer')
        list.innerText = JSON.stringify(commits)
    }

}




