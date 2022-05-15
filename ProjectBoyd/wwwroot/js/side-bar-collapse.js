// booleans
    let isMobile = false;

    let sideMenuOpen = true;
    let sideMenuSession = 0;

// integers
    const mobileScreenWidth = 700 // minimum window width before mobile mode is enabled

// arrays 

    const elements = {

        sideContainer: "side-container",
        sideContainerMask: "side-container-bg-mask",

        sideContainerToggle: "menu-toggle2",
        navBarToggle: "menu-toggle",

        bodyMain: "body-main"

    }

// functions

function toggleScrolling (scrollingEnabled) {
    if (scrollingEnabled) {
      //disable
      var x=window.scrollX;
      var y=window.scrollY;
      window.touchmove=function(){window.scrollTo(x,y);};
      window.onscroll=function(){window.scrollTo(x, y);};
      
    }
    else {
      // enable
       window.onscroll=function(){};
    }
  }

function hideOnClickOutside(element,callback) {
    const outsideClickListener = event => {
        if (!element.contains(event.target)) { // or use: event.target.closest(selector) === null
          const result = callback()
          removeClickListener()
        }
    }

    const removeClickListener = () => {
       document.removeEventListener('click', outsideClickListener)
    }

    document.addEventListener('click', outsideClickListener)
}

function toggleBodySize(toggle) {
    
    const change = [
      elements.bodyMain
    ]

    const width = toggle && "calc(100% - 240px)" || "100%"
    const left = toggle && "240px" || "0px"
    
    change.forEach((element) => {

      element.style.width = width
      element.style.left = left
      
      
    })
    
    
  }

function toggleMenu (visible) {
    const isNull = visible == null
    const isBoolean = typeof(visible) == "boolean"
    
    
    sideMenuOpen = (!isNull && isBoolean) && visible || (isNull || !isBoolean) && !sideMenuOpen
    
    
    // hide toggle
    elements.navBarToggle.style.display = sideMenuOpen && "none" || "block"
    
    
    const ele = elements.sideContainer
    ele.style.left = sideMenuOpen && "0px" || "-240px"
    
    const sessionId = sideMenuOpen && new Date().valueOf() || 0
    
    sideMenuSession = sessionId // apply the session
    
    // do different things depending on device

    if (isMobile) {
      const mask = elements.sideContainerMask
      
      if (sideMenuOpen) {
        
        mask.classList.add("active")
        
        toggleScrolling(true)
        
        
        // click closer
          setTimeout(hideOnClickOutside,0,ele,() => {
            if (sideMenuOpen && sideMenuSession == sessionId) {
              toggleMenu(false)
            }
          })
      
      }
      else {
        
        mask.classList.remove("active")
        
        setTimeout(() => {
          if (!sideMenuOpen && isMobile) {
            toggleScrolling(false)
          }
        },200)
        
      }
      
      
    }
    else {
      toggleBodySize(sideMenuOpen)
    }
   
    
    
    
  }

  function windowSizeChanged (entries) {
    let width = entries[0].target.clientWidth
    if (mobileScreenWidth >= width && !isMobile) {
      // enable mobile mode
      isMobile = true
      
      // set main container size
        toggleBodySize(false)
      
      // close side menu
        toggleMenu(false)
    
      // enable scrolling
        toggleScrolling(false)
  
      
    }
    else if (width > mobileScreenWidth && isMobile) {
      // disable mobile mode
      isMobile = false
      
      // hide bg mask
        const mask = elements.sideContainerMask
        mask.classList.remove("active")
      
      // open side menu
        toggleMenu(true)
      
      // close account modal and enable scrolling
        toggleScrolling(false)
      
    }
  }


window.onload = function () {


    
    for (let [name, id] of Object.entries(elements)) {

        elements[name] = document.getElementById(id)

    }


    elements.sideContainerToggle.onclick = toggleMenu
    elements.navBarToggle.onclick = toggleMenu

    // observe page size for mobile resizing
        new ResizeObserver(entries => windowSizeChanged(entries)).observe(document.body)

    elements.bodyMain.appendChild(elements.sideContainerMask)

}

