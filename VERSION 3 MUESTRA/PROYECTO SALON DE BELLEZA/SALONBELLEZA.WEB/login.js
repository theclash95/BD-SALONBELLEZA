document.querySelector('#login-form').addEventListener('submit',e=>{
 e.preventDefault();
 const user=document.querySelector('#login-user').value.trim().toLowerCase();
 const pass=document.querySelector('#login-password').value;
 const error=document.querySelector('#login-error');
 if(user==='admin@atelier.com' && pass==='Atelier2026!'){
   sessionStorage.setItem('atelier_barber_demo_session','active');
   location.href='inicio.html';
 }else error.textContent='Usuario o contraseña incorrectos. Revisá los datos e intentá nuevamente.';
});
document.querySelector('#toggle-password').addEventListener('click',()=>{
 const input=document.querySelector('#login-password');input.type=input.type==='password'?'text':'password';
});