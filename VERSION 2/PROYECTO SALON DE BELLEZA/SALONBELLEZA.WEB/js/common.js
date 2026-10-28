// Acceso web de demostración. No reemplaza una autenticación segura en el servidor.
(function(){
 const page=(location.pathname.split('/').pop()||'index.html').toLowerCase();
 if(page!=='login.html' && sessionStorage.getItem('barberia_web_session')!=='active'){
   location.replace('login.html');
 }
})();
const API_URL = "https://localhost:7180";
const navItems = [
  ["index.html","⌂","Panel principal"], ["turnos.html","◷","Turnos"],
  ["clientes.html","♙","Clientes"], ["empleados.html","✂","Barberos y equipo"], ["servicios.html","✧","Servicios"]
];
function renderSidebar(active){
 const el=document.getElementById('sidebar'); if(!el)return;
 el.innerHTML=`<div class="brand"><strong>BARBERÍA</strong><small>Gestión profesional</small></div><div class="nav-label">Menú principal</div><nav>${navItems.map(([href,icon,label])=>`<a href="${href}" class="${href===active?'active':''}"><span class="nav-icon">${icon}</span><span>${label}</span></a>`).join('')}</nav><div class="side-foot">Sistema de administración<br>Barbería · sistema de gestión<br><button class="logout-btn" type="button" onclick="cerrarSesionWeb()">↪ Cerrar sesión</button></div>`;
}
async function apiRequest(path, options={}){
 const response=await fetch(`${API_URL}${path}`,{...options,headers:{...(options.body?{'Content-Type':'application/json'}:{}),...(options.headers||{})}});
 const raw=await response.text(); let data=null; if(raw){try{data=JSON.parse(raw)}catch{data=raw}}
 if(!response.ok){const message=(data&&typeof data==='object'&&(data.mensaje||data.message||data.title||data.detalle))||`Error HTTP ${response.status}`; throw new Error(message)}
 return data;
}
function escapeText(value){return String(value??'').replace(/[&<>"']/g,ch=>({'&':'&amp;','<':'&lt;','>':'&gt;','"':'&quot;',"'":'&#39;'}[ch]));}
function badge(estado){const s=String(estado??'SIN ESTADO').trim().toLowerCase();return `<span class="status ${escapeText(s)}">${escapeText(s.toUpperCase())}</span>`}
function showMessage(el,text,type=''){if(!el)return;el.className=`message ${type}`;el.textContent=text}
function formatDate(value){if(!value)return '—';const s=String(value).slice(0,10);const p=s.split('-');return p.length===3?`${p[2]}/${p[1]}/${p[0]}`:s}

function cerrarSesionWeb(){
 sessionStorage.removeItem('barberia_web_session');
 location.replace('login.html');
}
