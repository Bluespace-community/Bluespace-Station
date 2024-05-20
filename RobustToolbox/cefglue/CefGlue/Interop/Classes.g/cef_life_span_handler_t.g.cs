﻿//
// DO NOT MODIFY! THIS IS AUTOGENERATED FILE!
//
namespace Xilium.CefGlue.Interop
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Security;
    
    [StructLayout(LayoutKind.Sequential, Pack = libcef.ALIGN)]
    [SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
    internal unsafe struct cef_life_span_handler_t
    {
        internal cef_base_ref_counted_t _base;
        internal delegate* unmanaged<cef_life_span_handler_t*, cef_browser_t*, cef_frame_t*, cef_string_t*, cef_string_t*, CefWindowOpenDisposition, int, cef_popup_features_t*, cef_window_info_t*, cef_client_t**, cef_browser_settings_t*, cef_dictionary_value_t**, int*, int> _on_before_popup;
        internal delegate* unmanaged<cef_life_span_handler_t*, cef_browser_t*, cef_window_info_t*, cef_client_t**, cef_browser_settings_t*, cef_dictionary_value_t**, int*, void> _on_before_dev_tools_popup;
        internal delegate* unmanaged<cef_life_span_handler_t*, cef_browser_t*, void> _on_after_created;
        internal delegate* unmanaged<cef_life_span_handler_t*, cef_browser_t*, int> _do_close;
        internal delegate* unmanaged<cef_life_span_handler_t*, cef_browser_t*, void> _on_before_close;
        
        internal GCHandle _obj;
        
        [UnmanagedCallersOnly]
        public static void add_ref(cef_life_span_handler_t* self)
        {
            var obj = (CefLifeSpanHandler)self->_obj.Target;
            obj.add_ref(self);
        }
        
        [UnmanagedCallersOnly]
        public static int release(cef_life_span_handler_t* self)
        {
            var obj = (CefLifeSpanHandler)self->_obj.Target;
            return obj.release(self);
        }
        
        [UnmanagedCallersOnly]
        public static int has_one_ref(cef_life_span_handler_t* self)
        {
            var obj = (CefLifeSpanHandler)self->_obj.Target;
            return obj.has_one_ref(self);
        }
        
        [UnmanagedCallersOnly]
        public static int has_at_least_one_ref(cef_life_span_handler_t* self)
        {
            var obj = (CefLifeSpanHandler)self->_obj.Target;
            return obj.has_at_least_one_ref(self);
        }
        
        [UnmanagedCallersOnly]
        public static int on_before_popup(cef_life_span_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_string_t* target_url, cef_string_t* target_frame_name, CefWindowOpenDisposition target_disposition, int user_gesture, cef_popup_features_t* popupFeatures, cef_window_info_t* windowInfo, cef_client_t** client, cef_browser_settings_t* settings, cef_dictionary_value_t** extra_info, int* no_javascript_access)
        {
            var obj = (CefLifeSpanHandler)self->_obj.Target;
            return obj.on_before_popup(self, browser, frame, target_url, target_frame_name, target_disposition, user_gesture, popupFeatures, windowInfo, client, settings, extra_info, no_javascript_access);
        }
        
        [UnmanagedCallersOnly]
        public static void on_before_dev_tools_popup(cef_life_span_handler_t* self, cef_browser_t* browser, cef_window_info_t* windowInfo, cef_client_t** client, cef_browser_settings_t* settings, cef_dictionary_value_t** extra_info, int* use_default_window)
        {
            var obj = (CefLifeSpanHandler)self->_obj.Target;
            obj.on_before_dev_tools_popup(self, browser, windowInfo, client, settings, extra_info, use_default_window);
        }
        
        [UnmanagedCallersOnly]
        public static void on_after_created(cef_life_span_handler_t* self, cef_browser_t* browser)
        {
            var obj = (CefLifeSpanHandler)self->_obj.Target;
            obj.on_after_created(self, browser);
        }
        
        [UnmanagedCallersOnly]
        public static int do_close(cef_life_span_handler_t* self, cef_browser_t* browser)
        {
            var obj = (CefLifeSpanHandler)self->_obj.Target;
            return obj.do_close(self, browser);
        }
        
        [UnmanagedCallersOnly]
        public static void on_before_close(cef_life_span_handler_t* self, cef_browser_t* browser)
        {
            var obj = (CefLifeSpanHandler)self->_obj.Target;
            obj.on_before_close(self, browser);
        }
        
        internal static cef_life_span_handler_t* Alloc(CefLifeSpanHandler obj)
        {
            var ptr = (cef_life_span_handler_t*)NativeMemory.Alloc((UIntPtr)sizeof(cef_life_span_handler_t));
            *ptr = default(cef_life_span_handler_t);
            ptr->_base._size = (UIntPtr)sizeof(cef_life_span_handler_t);
            ptr->_obj = GCHandle.Alloc(obj);
            ptr->_base._add_ref = (delegate* unmanaged<cef_base_ref_counted_t*, void>)(delegate* unmanaged<cef_life_span_handler_t*, void>)&add_ref;
            ptr->_base._release = (delegate* unmanaged<cef_base_ref_counted_t*, int>)(delegate* unmanaged<cef_life_span_handler_t*, int>)&release;
            ptr->_base._has_one_ref = (delegate* unmanaged<cef_base_ref_counted_t*, int>)(delegate* unmanaged<cef_life_span_handler_t*, int>)&has_one_ref;
            ptr->_base._has_at_least_one_ref = (delegate* unmanaged<cef_base_ref_counted_t*, int>)(delegate* unmanaged<cef_life_span_handler_t*, int>)&has_at_least_one_ref;
            ptr->_on_before_popup = &on_before_popup;
            ptr->_on_before_dev_tools_popup = &on_before_dev_tools_popup;
            ptr->_on_after_created = &on_after_created;
            ptr->_do_close = &do_close;
            ptr->_on_before_close = &on_before_close;
            return ptr;
        }
        
        internal static void Free(cef_life_span_handler_t* ptr)
        {
            ptr->_obj.Free();
            NativeMemory.Free((void*)ptr);
        }
        
    }
}
