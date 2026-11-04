/* Atelier Barber Club - frontend para SALONBELLEZA.API
   Configurá API_BASE según la URL donde se ejecute ASP.NET Core. */
const API_BASE = localStorage.getItem('salon_api_url') || 'http://localhost:5180';
const $ = (s, root=document) => root.querySelector(s);
const $$ = (s, root=document) => [...root.querySelectorAll(s)];
const state = { view:'dashboard', data:{}, search:'', editing:null, statusFilter:'TODOS' };
const resources = {
  clientes:{title:'Clientes',singular:'cliente',endpoint:'/api/Clientes',desc:'Administrá la información y el historial de tus clientes.',fields:[
    ['dni','DNI','text',true],['nombre','Nombre','text',true],['apellido','Apellido','text',true],['telefono','Teléfono','tel'],['email','Correo electrónico','email'],['direccion','Dirección','text'],['categoria','Categoría','text']
  ],columns:[['nombre','Cliente'],['dni','DNI'],['telefono','Teléfono'],['email','Correo'],['categoria','Categoría']],id:'idCliente'},
  empleados:{title:'Empleados',singular:'empleado',endpoint:'/api/Empleados',desc:'Gestioná el equipo de trabajo de la barbería.',fields:[
    ['dni','DNI','text',true],['nombre','Nombre','text',true],['apellido','Apellido','text',true],['telefono','Teléfono','tel'],['email','Correo electrónico','email'],['direccion','Dirección','text'],['tipoEmpleado','Tipo de empleado','text',true]
  ],columns:[['nombre','Empleado'],['dni','DNI'],['telefono','Teléfono'],['email','Correo'],['tipoEmpleado','Puesto']],id:'idEmpleado'},
  servicios:{title:'Servicios',singular:'servicio',endpoint:'/api/Servicios',desc:'Definí los tratamientos, sus precios y duración.',fields:[
    ['idCategoriaServicio','ID categoría','number',true],['nombre','Nombre del servicio','text',true],['descripcion','Descripción','text'],['precio','Precio','number',true],['duracionMinuto','Duración (minutos)','number',true]
  ],columns:[['nombre','Servicio'],['descripcion','Descripción'],['precio','Precio'],['duracionMinuto','Duración']],id:'idServicio'},
  productos:{title:'Productos',singular:'producto',endpoint:'/api/Productos',desc:'Controlá el catálogo, los precios y el stock.',fields:[
    ['idCategoriaProducto','ID categoría','number',true],['codigo','Código','text'],['nombre','Nombre','text',true],['descripcion','Descripción','text'],['precioCosto','Precio de costo','number',true],['precioVenta','Precio de venta','number',true],['stock','Stock actual','number',true],['stockMinimo','Stock mínimo','number',true]
  ],columns:[['codigo','Código'],['nombre','Producto'],['precioVenta','Precio venta'],['stockActual','Stock'],['stockMinimo','Stock mínimo']],id:'idProducto'},
  proveedores:{title:'Proveedores',singular:'proveedor',endpoint:'/api/Proveedores',desc:'Mantené actualizados los datos de tus proveedores.',fields:[
    ['razonSocial','Razón social','text',true],['cuit','CUIT','text'],['telefono','Teléfono','tel'],['email','Correo electrónico','email'],['direccion','Dirección','text']
  ],columns:[['razonSocial','Razón social'],['cuit','CUIT'],['telefono','Teléfono'],['email','Correo']],id:'idProveedor'},
  compras:{title:'Compras',singular:'compra',endpoint:'/api/Compras',desc:'Registrá el ingreso de mercadería y actualizá el inventario.',fields:[
    ['idProveedor','ID proveedor','number',true],['idProducto','ID producto','number',true],['cantidad','Cantidad','number',true],['precioCosto','Precio de costo','number',true],['observacion','Observación','text']
  ],columns:[['idCompra','N.º'],['idProveedor','Proveedor ID'],['idProducto','Producto ID'],['cantidad','Cantidad'],['precioCosto','Costo']],id:'idCompra'},
  ventas:{title:'Ventas',singular:'venta',endpoint:'/api/Ventas',desc:'Consultá las ventas generadas en la barbería.',fields:[],columns:[['idVenta','N.º venta'],['idCliente','Cliente ID'],['total','Total'],['fecha','Fecha'],['estado','Estado']],id:'idVenta'},
  facturas:{title:'Facturación',singular:'factura',endpoint:'/api/Facturas',desc:'Consultá y generá comprobantes de venta.',fields:[
    ['idVenta','ID de venta','number',true],['numeroFactura','Número de factura','text',true],['tipoComprobante','Tipo de comprobante','text',true]
  ],columns:[['idFactura','N.º'],['idVenta','Venta ID'],['numeroFactura','Comprobante'],['tipoComprobante','Tipo'],['fechaEmision','Fecha']],id:'idFactura'},
  membresias:{title:'Membresías',singular:'membresía',endpoint:'/api/Membresias',desc:'Configurá planes y beneficios para tus clientes.',fields:[
    ['nombrePlan','Nombre del plan','text',true],['descripcion','Descripción','text'],['costo','Costo','number',true],['duracionDias','Duración (días)','number',true],['porcentajeDescuento','Descuento (%)','number']
  ],columns:[['nombrePlan','Plan'],['descripcion','Descripción'],['costo','Costo'],['duracionDias','Duración'],['porcentajeDescuento','Descuento']],id:'idMembresia'}
};
const money = n => Number(n||0).toLocaleString('es-AR',{style:'currency',currency:'ARS',maximumFractionDigits:0});
const val = (obj,key) => obj?.[key] ?? obj?.[key[0]?.toUpperCase()+key.slice(1)] ?? '';
const first = (obj, keys) => { for(const k of keys){const v=val(obj,k);if(v!==''&&v!=null)return v;} return ''; };
function recordStatus(item){const raw=String(first(item,['estado','status','estadoRegistro','situacion'])||'').trim().toUpperCase();const active=first(item,['activo','isActive','habilitado','vigente']);if(['DESACTIVADO','INACTIVO','ELIMINADO','BAJA','ANULADO','ANULADA','CANCELADO','CANCELADA','0','FALSE'].includes(raw)||active===false||active===0||active==='false')return 'DESACTIVADO';if(['PENDIENTE','PENDING','EN ESPERA','RESERVADO','RESERVADA'].includes(raw))return 'PENDIENTE';if(['ACTIVO','ACTIVE','HABILITADO','VIGENTE','CONFIRMADO','CONFIRMADA','EN CURSO','PAGADA','PAGADO','COMPLETADA','COMPLETADO','APROBADO','APROBADA'].includes(raw)||active===true||active===1||active==='true')return 'ACTIVO';return raw||'ACTIVO';}
function statusPill(status){const s=String(status||'ACTIVO').toUpperCase();const cls=s==='ACTIVO'||s==='CONFIRMADO'||s==='APROBADO'?'green':s==='DESACTIVADO'||s==='CANCELADO'||s==='ANULADO'?'red':'amber';return `<span class="pill ${cls}">${escapeHtml(s)}</span>`;}
const initials = s => String(s||'?').split(' ').filter(Boolean).slice(0,2).map(x=>x[0]).join('').toUpperCase();
const dateText = d => {if(!d)return '—';const x=new Date(d);return isNaN(x)?String(d):x.toLocaleDateString('es-AR',{day:'2-digit',month:'short'});};
async function api(path, options={}) {
  const response = await fetch(API_BASE + path, { ...options, headers:{'Content-Type':'application/json',...(options.headers||{})} });
  const raw = await response.text();
  let body=null; try{body=raw?JSON.parse(raw):null}catch{body=raw}
  if(!response.ok){const message=body?.mensaje||body?.Mensaje||body?.title||body?.message||raw||`Error ${response.status}`;throw new Error(message);}
  return body;
}
function toast(message,error=false){const el=document.createElement('div');el.className='toast'+(error?' error':'');el.textContent=message;$('#toast-region').append(el);setTimeout(()=>el.remove(),3600);}
function setConnection(ok,label){$('#connection-dot').parentElement.className='connection '+(ok?'ok':'error');$('#connection-label').textContent=label;}
function pageHead(title,desc,action=''){return `<div class="page-heading"><div><div class="eyebrow">GESTIÓN DE BARBERÍA</div><h1>${title}</h1><p>${desc}</p></div>${action}</div>`;}
function actionButton(label,onclick='openCreate()'){return `<button class="btn btn-primary" onclick="${onclick}"><span>＋</span>${label}</button>`;}
function stat(label,value,note,icon){return `<article class="stat-card"><div class="stat-top"><span>${label}</span><span class="stat-icon">${icon}</span></div><div class="stat-value">${value}</div><div class="stat-note">${note}</div></article>`;}
function person(name,sub=''){return `<div class="person-cell"><span class="mini-avatar">${initials(name)}</span><span><b>${name||'Sin nombre'}</b><small>${sub||''}</small></span></div>`;}
function tableRows(items,resource){
  if(!items?.length)return `<tr><td colspan="10"><div class="empty">Todavía no hay registros para mostrar.</div></td></tr>`;
  return items.map(item=>{
    const id=val(item,resource.id);
    const cells=resource.columns.map(([key,label])=>{
      let v=val(item,key);
      if(key==='nombre' && resource===resources.clientes || key==='nombre' && resource===resources.empleados) return `<td>${person([val(item,'nombre'),val(item,'apellido')].filter(Boolean).join(' '),val(item,'email'))}</td>`;
      if(['precio','precioVenta','precioCosto','costo','total'].includes(key)&&v!=='')v=money(v);
      if(key.toLowerCase().includes('fecha'))v=dateText(v);
      if(key.toLowerCase().includes('estado'))v=statusPill(v||recordStatus(item));
      return `<td>${v===null||v===undefined||v===''?'—':escapeHtml(String(v))}</td>`;
    }).join('');
    const canEdit=['clientes','empleados','servicios','productos','proveedores'].includes(state.view);
    const isDeactivated=recordStatus(item)==='DESACTIVADO';
    const canDeactivate=canEdit && id!=='' && !isDeactivated;
    const canActivate=canEdit && id!=='' && isDeactivated;
    return `<tr>${cells}<td>${statusPill(recordStatus(item))}</td><td><div class="row-actions">${canEdit?`<button class="tiny-btn" onclick="editRecord('${state.view}',${Number(id)})">Editar</button>`:''}${canDeactivate?`<button class="tiny-btn danger-text" onclick="deleteRecord('${state.view}',${Number(id)})">Desactivar</button>`:''}${canActivate?`<button class="tiny-btn activate-text" onclick="activateRecord('${state.view}',${Number(id)})">Activar</button>`:''}</div></td></tr>`;
  }).join('');
}
function escapeHtml(s){return s.replace(/[&<>"']/g,c=>({'&':'&amp;','<':'&lt;','>':'&gt;','"':'&quot;',"'":'&#39;'}[c]));}
async function loadResource(key){
  const r=resources[key];const data=await api(r.endpoint);state.data[key]=Array.isArray(data)?data:(data?.items||data?.data||[]);
  renderResource(key);
}
function renderResource(key){
  const r=resources[key];const rows=state.data[key]||[];
  const filtered=rows.filter(x=>JSON.stringify(x).toLowerCase().includes(state.search.toLowerCase()) && (state.statusFilter==='TODOS'||recordStatus(x)===state.statusFilter));
  const action=r.fields.length?actionButton('Nuevo '+r.singular):'';
  $('#page-content').innerHTML=pageHead(r.title,r.desc,action)+`
    <div class="toolbar"><label class="search-box"><span>⌕</span><input id="table-search" placeholder="Buscar ${r.title.toLowerCase()}..." value="${escapeHtml(state.search)}"></label><div class="toolbar-actions"><select id="status-filter" class="status-filter"><option value="TODOS">Todos los estados</option><option value="PENDIENTE">Pendientes</option><option value="ACTIVO">Activos</option><option value="DESACTIVADO">Desactivados</option></select><button class="btn btn-light" onclick="refreshView()">↻ Actualizar</button></div></div>
    <section class="panel table-panel"><div class="table-wrap"><table><thead><tr>${r.columns.map(c=>`<th>${c[1].toUpperCase()}</th>`).join('')}<th>ESTADO</th><th>ACCIONES</th></tr></thead><tbody>${tableRows(filtered,r)}</tbody></table></div></section>`;
  $('#table-search').addEventListener('input',e=>{state.search=e.target.value;const pos=e.target.selectionStart;renderResource(key);$('#table-search').focus();$('#table-search').setSelectionRange(pos,pos);});
  $('#status-filter').value=state.statusFilter||'TODOS';$('#status-filter').addEventListener('change',e=>{state.statusFilter=e.target.value;renderResource(key);});
}
async function loadDashboard(){
  try{
    const d=await api('/api/Dashboard');
    const clients=Array.isArray(d.clientes||d.Clientes)?(d.clientes||d.Clientes):[];
    const products=Array.isArray(d.productos||d.Productos)?(d.productos||d.Productos):[];
    const staff=Array.isArray(d.empleados||d.Empleados)?(d.empleados||d.Empleados):[];
    const sales=Array.isArray(d.ventas||d.Ventas)?(d.ventas||d.Ventas):[];
    state.data={clientes:clients,productos:products,empleados:staff,ventas:sales};
    const low=products.filter(p=>Number(first(p,['stockActual','stock']))<=Number(val(p,'stockMinimo')||0));
    const totalSales=sales.reduce((a,s)=>a+Number(first(s,['total','montoTotal','importe'])||0),0);
    const recent=clients.slice(-5).reverse();
    const hour=new Date().getHours();
    const greeting=hour<12?'Buenos días':hour<19?'Buenas tardes':'Buenas noches';
    const todayLabel=new Date().toLocaleDateString('es-AR',{weekday:'long',day:'numeric',month:'long',year:'numeric'});
    $('#page-content').innerHTML=`
      <section class="home-hero">
        <div class="home-hero-copy"><span class="home-kicker">ATELIER BARBER CLUB</span><h1>${greeting},<br><em>bienvenido/a.</em></h1><p>Este es el centro de operaciones de tu barbería. Desde aquí podés organizar el trabajo, acompañar a tus clientes y controlar cada detalle.</p><div class="home-hero-actions"><button class="btn btn-home-primary" onclick="go('turnos')">＋ Crear / consultar turnos</button><button class="btn btn-home-ghost" onclick="go('clientes')">Ver clientes <span>→</span></button></div></div>
        <div class="home-hero-side"><div class="home-date-label">HOY EN ATELIER</div><div class="home-date">${todayLabel}</div><div class="home-date-divider"></div><div class="home-date-caption">Un espacio para organizar, crecer y brindar una mejor experiencia.</div><div class="home-decoration">a.</div></div>
      </section>
      <div class="home-section-heading"><div><span class="eyebrow">VISTA GENERAL</span><h2>Resumen de la barbería</h2></div><span class="home-live"><i></i> Panel actualizado</span></div>
      <div class="stats-grid">
        ${stat('CLIENTES REGISTRADOS',clients.length,'Personas en tu base de datos','♙')}
        ${stat('EMPLEADOS',staff.length,'Equipo de trabajo','♧')}
        ${stat('PRODUCTOS',products.length,low.length?`${low.length} con stock bajo`:'Inventario registrado','◇')}
        ${stat('VENTAS REGISTRADAS',sales.length,`Acumulado visible: ${money(totalSales)}`,'↗')}
      </div>
      <div class="dashboard-grid">
        <div>
          <section class="panel"><div class="panel-head"><div><h3>Clientes recientes</h3><p>Últimos registros disponibles</p></div><button class="text-link" onclick="go('clientes')">Ver todos →</button></div>
          <div class="table-wrap"><table><thead><tr><th>CLIENTE</th><th>TELÉFONO</th><th>CATEGORÍA</th></tr></thead><tbody>${recent.length?recent.map(c=>`<tr><td>${person([val(c,'nombre'),val(c,'apellido')].filter(Boolean).join(' '),val(c,'email'))}</td><td>${val(c,'telefono')||'—'}</td><td><span class="pill">${val(c,'categoria')||val(c,'categoriaCliente')||'General'}</span></td></tr>`).join(''):`<tr><td colspan="3"><div class="empty">No hay clientes registrados.</div></td></tr>`}</tbody></table></div></section>
          <section class="panel"><div class="panel-head"><div><h3>Accesos rápidos</h3><p>Atajos a las tareas habituales</p></div></div><div class="quick-grid">
            <button class="quick-action" onclick="go('turnos')"><span>▦</span>Administrar turnos</button>
            <button class="quick-action" onclick="go('clientes')"><span>♙</span>Registrar cliente</button>
            <button class="quick-action" onclick="go('productos')"><span>◇</span>Controlar stock</button>
            <button class="quick-action" onclick="go('ventas')"><span>↗</span>Consultar ventas</button>
          </div></section>
        </div>
        <div>
          <section class="panel"><div class="panel-head"><div><h3>Inventario a revisar</h3><p>Productos en el mínimo o por debajo</p></div><button class="text-link" onclick="go('productos')">Ver →</button></div>
          ${low.length?`<div class="agenda-list">${low.slice(0,6).map(p=>`<div class="agenda-item"><div class="agenda-time">${val(p,'stockActual')??val(p,'stock')??0} u.</div><div class="agenda-line"></div><div><b>${val(p,'nombre')||'Producto'}</b><small>Stock mínimo: ${val(p,'stockMinimo')||0}</small></div></div>`).join('')}</div>`:`<div class="empty">No se detectan alertas de stock.</div>`}</section>
          <section class="panel"><div class="panel-head"><div><h3>Estado del sistema</h3><p>Conexión con tu backend C#</p></div><span class="pill green">En línea</span></div><div class="metric-highlight">API SALONBELLEZA<strong>Conectada</strong><small>Datos obtenidos desde /api/Dashboard</small></div></section>
        </div>
      </div>`;
  }catch(e){setConnection(false,'API sin conexión');$('#page-content').innerHTML=pageHead('Inicio','No se pudo obtener el resumen del sistema.')+`<section class="panel"><div class="notice"><b>No pudimos conectar con tu API.</b><br>${escapeHtml(e.message)}<br><br>Verificá que ASP.NET Core esté ejecutándose en <b>${API_BASE}</b>, que MySQL esté activo y que la base de datos esté disponible. Podés cambiar la dirección en Configuración.</div><br><button class="btn btn-primary" onclick="refreshView()">Reintentar conexión</button></section>`;}
}
const viewNames={dashboard:'Inicio',turnos:'Turnos',clientes:'Clientes',servicios:'Servicios',empleados:'Empleados',productos:'Productos',proveedores:'Proveedores',compras:'Compras',ventas:'Ventas',pagos:'Pagos',facturas:'Facturación',membresias:'Membresías',configuracion:'Configuración'};
async function go(view){
  state.view=view;state.search='';$$('.nav-item').forEach(b=>b.classList.toggle('active',b.dataset.view===view));$('#page-crumb').textContent=viewNames[view]||view;$('#sidebar').classList.remove('open');
  if(view==='dashboard'){await loadDashboard();return;}
  if(view==='ventas'){await loadVentas();return;}
  if(view==='categorias'){await loadCategorias();return;}
  if(resources[view]){try{await loadResource(view);setConnection(true,'API conectada')}catch(e){renderError(e);setConnection(false,'API sin conexión')}return;}
  if(view==='turnos'){await loadTurnos();return;}
  if(view==='pagos'){await loadPagos();return;}
  if(view==='configuracion'){renderConfig();return;}
}
function renderError(e){$('#page-content').innerHTML=pageHead(viewNames[state.view]||'Gestión','No se pudo cargar la información.')+`<section class="panel"><div class="notice">${escapeHtml(e.message)}<br><br>Comprobá la conexión con la API y que el endpoint esté disponible.</div><br><button class="btn btn-primary" onclick="refreshView()">Reintentar</button></section>`;}
async function loadTurnos(){
  try{const rows=await api('/api/Turnos');state.data.turnos=Array.isArray(rows)?rows:(rows?.items||rows?.data||[]);renderTurnos();setConnection(true,'API conectada');}
  catch(e){renderError(e);setConnection(false,'API sin conexión');}
}
function renderTurnos(){
  const rows=state.data.turnos||[];
  const filtered=rows.filter(t=>JSON.stringify(t).toLowerCase().includes(state.search.toLowerCase()));
  $('#page-content').innerHTML=pageHead('Turnos','Organizá las citas y servicios de tus clientes.',actionButton('Nuevo turno','openTurnoCreate()'))+
    `<div class="toolbar"><label class="search-box">⌕ <input id="table-search" placeholder="Buscar por cliente, empleado o fecha..." value="${escapeHtml(state.search)}"></label><div class="toolbar-actions"><button class="btn btn-light" onclick="refreshView()">↻ Actualizar</button></div></div>
    <section class="panel table-panel"><div class="table-wrap"><table><thead><tr><th>N.º</th><th>CLIENTE</th><th>EMPLEADO</th><th>FECHA</th><th>HORA</th></tr></thead><tbody>${filtered.length?filtered.map(t=>{const id=first(t,['idTurno','id']);return `<tr><td>#${escapeHtml(String(id||'—'))}</td><td>${escapeHtml(String(first(t,['nombreCliente','cliente','idCliente'])||'—'))}</td><td>${escapeHtml(String(first(t,['nombreEmpleado','empleado','idEmpleado'])||'—'))}</td><td>${dateText(first(t,['fecha','fechaTurno']))}</td><td>${escapeHtml(String(first(t,['hora','horaTurno'])||'—'))}</td></tr>`;}).join(''):`<tr><td colspan="5"><div class="empty">No hay turnos para mostrar.</div></td></tr>`}</tbody></table></div></section>`;
  $('#table-search').addEventListener('input',e=>{state.search=e.target.value;renderTurnos();const input=$('#table-search');input.focus();input.setSelectionRange(input.value.length,input.value.length);});
}
async function openTurnoCreate(){
  try{
    const [clientes,empleados]=await Promise.all([api('/api/Clientes'),api('/api/Empleados')]);
    const cs=Array.isArray(clientes)?clientes:(clientes?.items||clientes?.data||[]), es=Array.isArray(empleados)?empleados:(empleados?.items||empleados?.data||[]);
    $('#modal-title').textContent='Nuevo turno';$('#modal-kicker').textContent='AGENDA';
    const options=(arr,idKey)=>arr.filter(x=>recordStatus(x)!=='DESACTIVADO').map(x=>`<option value="${val(x,idKey)}">${escapeHtml([val(x,'nombre'),val(x,'apellido')].filter(Boolean).join(' ')||val(x,'razonSocial')||('#'+val(x,idKey)))}</option>`).join('');
    $('#form-fields').innerHTML=`<div class="field"><label for="turno-cliente">Cliente *</label><select id="turno-cliente" required><option value="">Seleccioná un cliente</option>${options(cs,'idCliente')}</select></div><div class="field"><label for="turno-empleado">Barbero / empleado *</label><select id="turno-empleado" required><option value="">Seleccioná un empleado</option>${options(es,'idEmpleado')}</select></div><div class="field"><label for="turno-fecha">Fecha *</label><input id="turno-fecha" type="date" required min="${new Date().toISOString().slice(0,10)}"></div><div class="field"><label for="turno-hora">Hora *</label><input id="turno-hora" type="time" required></div><div class="field"><label for="turno-estado">Estado</label><select id="turno-estado"><option value="Pendiente">Pendiente</option><option value="Activo">Activo</option></select></div>`;
    $('#modal-backdrop').hidden=false;
  }catch(e){toast('No se pudo preparar el formulario de turnos: '+e.message,true);}
}
async function saveTurno(e){
  e.preventDefault();const data={idCliente:Number($('#turno-cliente').value),idEmpleado:Number($('#turno-empleado').value),fecha:$('#turno-fecha').value,hora:$('#turno-hora').value,estado:$('#turno-estado').value};
  if(!data.idCliente||!data.idEmpleado||!data.fecha||!data.hora)return toast('Completá cliente, empleado, fecha y hora.',true);
  try{await api('/api/Turnos',{method:'POST',body:JSON.stringify(data)});closeModal();$('#record-form').onsubmit=null;toast('Turno creado correctamente.');state.search='';state.statusFilter='TODOS';await loadTurnos();}
  catch(err){toast('No se pudo crear el turno: '+err.message,true);}
}
async function deleteTurno(id){if(!confirm('¿Querés cancelar o desactivar este turno?'))return;try{await api('/api/Turnos/'+id,{method:'DELETE'});toast('Turno desactivado.');await loadTurnos();}catch(e){toast('La API no permitió desactivar el turno: '+e.message,true);}}



async function loadCategorias(){
  $('#page-content').innerHTML=pageHead('Categorías','Organizá las categorías de los productos y servicios de la barbería.')+`<div class="dashboard-grid"><section class="panel"><div class="panel-head"><div><h3>Categorías de productos</h3><p>Clasificación del inventario</p></div></div><div id="cat-productos" class="empty">Cargando categorías…</div></section><section class="panel"><div class="panel-head"><div><h3>Categorías de servicios</h3><p>Clasificación de los tratamientos</p></div></div><div id="cat-servicios" class="empty">Cargando categorías…</div></section></div>`;
  const renderList=(items)=>{const arr=Array.isArray(items)?items:(items?.items||items?.data||[]);if(!arr.length)return '<div class="empty">No hay categorías registradas.</div>';return `<div class="agenda-list">${arr.map((x,i)=>`<div class="agenda-item"><div class="agenda-time">${String(i+1).padStart(2,'0')}</div><div class="agenda-line"></div><div><b>${escapeHtml(String(first(x,['nombre','descripcion','categoria'])||'Categoría'))}</b><small>${escapeHtml(String(first(x,['descripcion','detalle'])||''))}</small></div></div>`).join('')}</div>`;};
  try{const [prod,serv]=await Promise.all([api('/api/Categorias/producto'),api('/api/Categorias/servicio')]);$('#cat-productos').innerHTML=renderList(prod);$('#cat-servicios').innerHTML=renderList(serv);setConnection(true,'API conectada');}
  catch(e){$('#cat-productos').innerHTML='<div class="notice">No se pudieron cargar las categorías. Verificá los endpoints de categorías.</div>';$('#cat-servicios').innerHTML='<div class="notice">No se pudieron cargar las categorías. Verificá los endpoints de categorías.</div>';toast(e.message,true);}
}

async function loadVentas(){
  try{
    const response=await api('/api/Ventas');
    state.data.ventas=Array.isArray(response)?response:(response?.items||response?.data||[]);
    renderVentas();
    setConnection(true,'API conectada');
  }catch(e){renderError(e);setConnection(false,'API sin conexión');}
}
function renderVentas(){
  const rows=state.data.ventas||[];
  const filtered=rows.filter(v=>JSON.stringify(v).toLowerCase().includes(state.search.toLowerCase())&&(state.statusFilter==='TODOS'||recordStatus(v)===state.statusFilter));
  const total=filtered.reduce((sum,v)=>sum+Number(first(v,['total','montoTotal','importe'])||0),0);
  const pendientes=filtered.filter(v=>String(val(v,'estado')||'').toUpperCase()==='PENDIENTE').length;
  $('#page-content').innerHTML=pageHead('Ventas','Consultá las ventas generadas en la barbería.')+`
    <div class="sales-summary">
      <div class="sales-summary-item"><span>VENTAS REGISTRADAS</span><strong>${filtered.length}</strong><small>Operaciones encontradas</small></div>
      <div class="sales-summary-item"><span>IMPORTE TOTAL</span><strong>${money(total)}</strong><small>Total de las ventas visibles</small></div>
      <div class="sales-summary-item"><span>PENDIENTES</span><strong>${pendientes}</strong><small>Ventas pendientes de pago</small></div>
    </div>
    <div class="toolbar"><label class="search-box"><span>⌕</span><input id="sales-search" placeholder="Buscar por venta, cliente o estado..." value="${escapeHtml(state.search)}"></label><div class="toolbar-actions"><select id="status-filter" class="status-filter"><option value="TODOS">Todos los estados</option><option value="PENDIENTE">Pendientes</option><option value="ACTIVO">Activos / completados</option><option value="DESACTIVADO">Anulados</option></select><button class="btn btn-light" onclick="refreshView()">↻ Actualizar</button></div></div>
    <section class="panel table-panel sales-table-panel"><div class="table-wrap"><table><thead><tr><th>N.º VENTA</th><th>CLIENTE</th><th>TOTAL</th><th>FECHA</th><th>ESTADO</th><th>ACCIONES</th></tr></thead><tbody>
    ${filtered.length?filtered.map(v=>{
      const id=first(v,['idVenta','ventaId','id'])||'—';
      const client=first(v,['nombreCliente','clienteNombre','idCliente'])||'—';
      const amount=first(v,['total','montoTotal','importe']);
      const date=first(v,['fechaVenta','fechaCreacion','fechaRegistro','fechaHora','createdAt','fecha']);
      const status=String(val(v,'estado')||'PENDIENTE').toUpperCase();
      const cls=status==='PAGADA'||status==='APROBADA'||status==='COMPLETADA'?'green':(status==='ANULADA'||status==='RECHAZADA'?'red':'amber');
      return `<tr><td><b class="sale-number">#${escapeHtml(String(id))}</b></td><td>${escapeHtml(String(client))}</td><td><b class="sale-total">${money(amount)}</b></td><td>${dateText(date)}</td><td><span class="pill ${cls}">${escapeHtml(status)}</span></td><td><button class="tiny-btn sale-action" onclick="verPagosVenta('${escapeHtml(String(id))}')">Ver pagos</button></td></tr>`;
    }).join(''):`<tr><td colspan="6"><div class="empty">${rows.length?'No hay ventas que coincidan con la búsqueda.':'Todavía no hay ventas registradas.'}</div></td></tr>`}
    </tbody></table></div></section>`;
  $('#sales-search').addEventListener('input',e=>{state.search=e.target.value;const pos=e.target.selectionStart;renderVentas();$('#sales-search').focus();$('#sales-search').setSelectionRange(pos,pos);});
  $('#status-filter').value=state.statusFilter||'TODOS';$('#status-filter').addEventListener('change',e=>{state.statusFilter=e.target.value;renderVentas();});
}
function verPagosVenta(id){
  go('pagos').then(()=>{const input=$('#sale-id');if(input){input.value=id;consultarPagos();}});
}

async function loadPagos(){
  $('#page-content').innerHTML=pageHead('Pagos','Controlá los cobros registrados y consultá los pagos de cada venta.')+`
    <section class="panel payments-search-panel">
      <div class="panel-head"><div><h3>Consultar pagos de una venta</h3><p>Ingresá el número de venta para ver sus movimientos.</p></div><span class="payment-search-icon">＄</span></div>
      <div class="payment-search-row">
        <label class="payment-input-wrap"><span>NÚMERO DE VENTA</span><input id="sale-id" type="number" min="1" placeholder="Ej. 125"></label>
        <button class="btn btn-primary payment-search-btn" onclick="consultarPagos()">⌕ &nbsp; Consultar pagos</button>
      </div>
    </section>
    <section id="payments-result" class="payments-result">
      <div class="payments-empty"><div class="empty-icon">＄</div><h3>Consultá los pagos de una venta</h3><p>Los movimientos aparecerán aquí con su fecha, importe, método y estado.</p></div>
    </section>`;
  $('#sale-id').addEventListener('keydown',e=>{if(e.key==='Enter')consultarPagos();});
}
async function consultarPagos(){
  const id=$('#sale-id')?.value;
  if(!id)return toast('Ingresá el número de venta.',true);
  const result=$('#payments-result');
  result.innerHTML='<section class="panel"><div class="loading"><div class="spinner"></div><span>Buscando pagos…</span></div></section>';
  try{
    const response=await api('/api/Pagos/venta/'+id);
    const rows=Array.isArray(response)?response:(response?.items||response?.data||(response?[response]:[]));
    const total=rows.reduce((sum,p)=>sum+Number(first(p,['monto','importe','total'])||0),0);
    const approved=rows.filter(p=>String(val(p,'estado')||'').toUpperCase()==='APROBADO').length;
    result.innerHTML=`
      <div class="payments-result-head"><div><div class="eyebrow">RESULTADO DE BÚSQUEDA</div><h2>Pagos de la venta <span>#${escapeHtml(String(id))}</span></h2><p>${rows.length} movimiento${rows.length===1?'':'s'} encontrado${rows.length===1?'':'s'}</p></div><button class="btn btn-light" onclick="consultarPagos()">↻ Actualizar</button></div>
      <div class="payment-stats">
        <article class="payment-stat"><span>TOTAL REGISTRADO</span><strong>${money(total)}</strong><small>Importe de los movimientos encontrados</small></article>
        <article class="payment-stat"><span>MOVIMIENTOS</span><strong>${rows.length}</strong><small>Pagos asociados a esta venta</small></article>
        <article class="payment-stat"><span>APROBADOS</span><strong>${approved}</strong><small>Pagos con estado aprobado</small></article>
      </div>
      <section class="panel payment-table-panel"><div class="panel-head"><div><h3>Detalle de movimientos</h3><p>Información devuelta por el sistema</p></div><span class="pill green">${rows.length} registros</span></div>
        <div class="table-wrap"><table class="payments-table"><thead><tr><th>OPERACIÓN</th><th>FECHA</th><th>MÉTODO DE PAGO</th><th>REFERENCIA</th><th>IMPORTE</th><th>ESTADO</th></tr></thead><tbody>
        ${rows.length?rows.map(p=>{
          const pid=first(p,['idPago'])||'—', date=first(p,['fechaPago','fecha','createdAt']), method=first(p,['metodoPago','medioPago'])||'—', ref=val(p,'referencia')||'—', amount=first(p,['monto','importe','total']), status=String(val(p,'estado')||'Pendiente').toUpperCase();
          const cls=status==='APROBADO'?'green':(status==='RECHAZADO'?'red':'amber');
          return `<tr><td><b class="payment-id">#${escapeHtml(String(pid))}</b></td><td>${dateText(date)}</td><td><span class="method-chip">${escapeHtml(String(method))}</span></td><td class="reference-cell">${escapeHtml(String(ref))}</td><td><b class="payment-amount">${money(amount)}</b></td><td><span class="pill ${cls}">${escapeHtml(status)}</span></td></tr>`;
        }).join(''):`<tr><td colspan="6"><div class="empty">No se encontraron pagos para la venta #${escapeHtml(String(id))}.</div></td></tr>`}
        </tbody></table></div></section>`;
  }catch(e){
    result.innerHTML=`<section class="panel"><div class="notice"><b>No se pudieron consultar los pagos.</b><br>${escapeHtml(e.message)}</div></section>`;
    toast(e.message,true);
  }
}
function renderConfig(){
 $('#page-content').innerHTML=pageHead('Configuración','Conectá la interfaz con tu backend ASP.NET Core.')+`<section class="panel config-card"><h3>Dirección de la API</h3><p class="muted">Ingresá la URL base donde se está ejecutando SALONBELLEZA.API. No agregues /api al final.</p><input id="api-url" value="${escapeHtml(API_BASE)}" placeholder="http://localhost:5180"><div><button class="btn btn-primary" onclick="saveApiUrl()">Guardar dirección</button> <button class="btn btn-light" onclick="testApi()">Probar conexión</button></div><div class="notice">Configuración inicial detectada en tu proyecto: <b>http://localhost:5180</b> (HTTP) y <b>https://localhost:7180</b> (HTTPS). Si usás HTTPS con certificado local, el navegador puede pedirte que lo aceptes primero.</div></section><section class="panel"><h3>Arquitectura</h3><p class="muted">Frontend web → ASP.NET Core API → CORE / servicios → Infrastructure / repositorios → MySQL.</p></section>`;
}
function saveApiUrl(){const v=$('#api-url').value.trim().replace(/\/$/,'');if(!/^https?:\/\//.test(v))return toast('Ingresá una URL válida.',true);localStorage.setItem('salon_api_url',v);toast('Dirección guardada.');location.reload();}
async function testApi(){try{await api('/api/Salon/estado');setConnection(true,'API conectada');toast('Conexión con la API exitosa.');}catch(e){setConnection(false,'API sin conexión');toast(e.message,true);}}
function refreshView(){go(state.view);}
function openCreate(){const key=state.view,r=resources[key];if(!r?.fields?.length)return;state.editing=null;$('#record-form').onsubmit=null;openModal('Nuevo '+r.singular,r.fields,{});}
function openModal(title,fields,record){
 $('#modal-title').textContent=title;$('#modal-kicker').textContent=state.view.toUpperCase();$('#form-fields').innerHTML=fields.map(([key,label,type,required])=>`<div class="field ${type==='textarea'?'full':''}"><label for="f-${key}">${label}${required?' *':''}</label>${type==='textarea'?`<textarea id="f-${key}" name="${key}"></textarea>`:`<input id="f-${key}" name="${key}" type="${type}" ${required?'required':''} ${type==='number'?'step="0.01"':''} value="${record[key]??''}">`}</div>`).join('');
 Object.entries(record||{}).forEach(([k,v])=>{const el=$(`#f-${k}`);if(el)el.value=v??'';});
 $('#modal-backdrop').hidden=false;
}
function closeModal(){$('#modal-backdrop').hidden=true;$('#record-form').reset();}
async function editRecord(key,id){const r=resources[key];try{const item=await api(r.endpoint+'/'+id);state.editing={key,id};$('#record-form').onsubmit=null;openModal('Editar '+r.singular,r.fields,item);}catch(e){toast(e.message,true);}}
async function deleteRecord(key,id){if(!confirm('¿Querés desactivar este registro?'))return;try{await api(resources[key].endpoint+'/'+id,{method:'DELETE'});toast('Registro desactivado.');await loadResource(key);}catch(e){toast(e.message,true);}}
async function activateRecord(key,id){
 if(!confirm('¿Querés volver a activar este registro?'))return;
 try{
   const endpoint=resources[key].endpoint;
   const current=await api(endpoint+'/'+id);
   const payload={...current};
   ['Estado','estadoRegistro','EstadoRegistro','status','Status','Activo','isActive','habilitado','vigente','EstadoActivo'].forEach(k=>delete payload[k]);
   payload.estado='Activo';payload.activo=true;
   await api(endpoint+'/'+id,{method:'PUT',body:JSON.stringify(payload)});
   await loadResource(key);
   const updated=(state.data[key]||[]).find(x=>String(val(x,resources[key].id))===String(id));
   if(updated && recordStatus(updated)!=='DESACTIVADO') toast('Registro activado correctamente.');
   else toast('La API aceptó la actualización, pero el estado sigue desactivado. Revisá el DTO y el método de actualización del backend para permitir modificar el estado.',true);
 }catch(e){toast('No se pudo activar el registro: '+e.message,true);}
}
$('#record-form').addEventListener('submit',async e=>{
 e.preventDefault();const key=state.view;if(key==='turnos'){await saveTurno(e);return;}const r=resources[key],data={};r.fields.forEach(([k,label,type])=>{const el=$(`#f-${k}`);if(!el)return;data[k]=type==='number'?(el.value===''?null:Number(el.value)):el.value.trim();});
 let method='POST',url=r.endpoint;
 if(state.editing){method='PUT';url+='/'+state.editing.id;
   if(key==='clientes')data.idCliente=state.editing.id;
   if(key==='empleados')data.idEmpleado=state.editing.id;
   if(key==='productos')data.idProducto=state.editing.id;
 }
 try{await api(url,{method,body:JSON.stringify(data)});closeModal();toast(state.editing?'Registro actualizado.':'Registro creado.');state.editing=null;await loadResource(key);}
 catch(err){toast(err.message,true);}
});
$('#modal-close').onclick=closeModal;$('#modal-cancel').onclick=closeModal;$('#modal-backdrop').addEventListener('click',e=>{if(e.target.id==='modal-backdrop')closeModal();});
$('#refresh-btn').onclick=refreshView;$('#mobile-menu').onclick=()=>$('#sidebar').classList.toggle('open');
// La navegación entre módulos utiliza páginas HTML independientes.
$('#today-label').textContent=new Date().toLocaleDateString('es-AR',{weekday:'long',day:'numeric',month:'long'});
const initialPage=document.body.dataset.page||'dashboard';
$('#today-label').textContent=new Date().toLocaleDateString('es-AR',{weekday:'long',day:'numeric',month:'long'});
$('#logout-btn').addEventListener('click',()=>{
  sessionStorage.removeItem('atelier_barber_demo_session');
  location.href='login.html';
});
if(!sessionStorage.getItem('atelier_barber_demo_session')){
  location.replace('login.html');
}else{
  (async()=>{
    try{await api('/api/Salon/estado');setConnection(true,'API conectada');}
    catch{setConnection(false,'API sin conexión');}
    await go(initialPage);
  })();
}
