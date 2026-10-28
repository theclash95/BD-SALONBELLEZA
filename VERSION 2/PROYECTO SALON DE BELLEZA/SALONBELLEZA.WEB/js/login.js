// Login solo del lado del navegador para demostración.
const DEMO_USER = "admin";
const DEMO_PASSWORD = "Barberia2026!";
const form = document.getElementById("loginForm");
const error = document.getElementById("loginError");
document.getElementById("togglePass").addEventListener("click",()=>{
 const field=document.getElementById("clave");
 field.type=field.type==="password"?"text":"password";
});
form.addEventListener("submit",event=>{
 event.preventDefault();
 const user=document.getElementById("usuario").value.trim();
 const pass=document.getElementById("clave").value;
 if(user===DEMO_USER && pass===DEMO_PASSWORD){
   sessionStorage.setItem("barberia_web_session","active");
   location.replace("index.html");
 }else{
   error.textContent="Usuario o contraseña incorrectos. Verificá los datos e intentá nuevamente.";
 }
});
if(sessionStorage.getItem("barberia_web_session")==="active") location.replace("index.html");
